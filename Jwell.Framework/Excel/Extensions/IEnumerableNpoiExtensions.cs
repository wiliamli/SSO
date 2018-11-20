using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace Jwell.Framework.Excel
{
    public static class IEnumerableNpoiExtensions
    {
        public static byte[] ToExcelContent<T>(this IEnumerable<T> source,string excelFile, string sheetName = "sheet0")
        {
            byte[] data = null;
            try
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                var book = source.ToWorkbook(excelFile, sheetName);

                using (var ms = new MemoryStream())
                {
                    book.Write(ms);
                    data = ms.GetBuffer();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public static void ToExcel<T>(this IEnumerable<T> source, string excelFile, string sheetName = "sheet0") where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (Path.GetExtension(excelFile).Equals(".xls"))
            {
                ExcelHelper.Setting.UserXlsx = false;
            }
            else if (Path.GetExtension(excelFile).Equals(".xlsx"))
            {
                ExcelHelper.Setting.UserXlsx = false;
            }
            else
            {
                throw new NotSupportedException($"不是Excel的扩展名 (*.xls | *.xlsx) {excelFile}");
            }

            try
            {
                var book = source.ToWorkbook(excelFile, sheetName);

                using (var stream = new FileStream(excelFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    book.Write(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static IWorkbook ToWorkbook<T>(this IEnumerable<T> source, string excelFile, string sheetName)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            bool fluentConfigEnabled = false;

            IFluentConfiguration fluentConfig;

            if (ExcelHelper.Setting.FluentConfigs.TryGetValue(typeof(T), out fluentConfig))
            {
                fluentConfigEnabled = true;
            }

            var cellConfigs = new CellConfig[properties.Length];
            for (var j = 0; j < properties.Length; j++)
            {
                var property = properties[j];

                PropertyConfiguration pc;
                if (fluentConfigEnabled && fluentConfig.PropertyConfigs.TryGetValue(property, out pc))
                {
                    cellConfigs[j] = pc.CellConfig;
                }
                else
                {
                    var attrs = property.GetCustomAttributes(typeof(ColumnAttribute), true) as ColumnAttribute[];
                    if (attrs != null && attrs.Length > 0)
                    {
                        cellConfigs[j] = attrs[0].CellConfig;
                    }
                    else
                    {
                        cellConfigs[j] = null;
                    }
                }
            }

            var workbook = InitializeWorkbook(excelFile);

            var sheet = workbook.CreateSheet(sheetName);

            var cellStyles = new Dictionary<int, ICellStyle>();

            var titleStyle = workbook.CreateCellStyle();
            titleStyle.Alignment = HorizontalAlignment.Center;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;
            titleStyle.FillPattern = FillPattern.SolidForeground;
            titleStyle.BorderBottom = BorderStyle.Thin;
            titleStyle.BorderLeft = BorderStyle.Thin;
            titleStyle.BorderRight = BorderStyle.Thin;
            titleStyle.FillForegroundColor = HSSFColor.Lime.Index;

            var titleRow = sheet.CreateRow(0);
            var rowIndex = 1;
            foreach (var item in source)
            {
                var row = sheet.CreateRow(rowIndex);
                for (var i = 0; i < properties.Length; i++)
                {
                    var property = properties[i];

                    int index = i;
                    var config = cellConfigs[i];
                    if (config != null)
                    {
                        if (config.IsIgnored)
                            continue;

                        index = config.Index;
                    }

                    if (rowIndex == 1)
                    {
                        var title = property.Name;
                        if (!string.IsNullOrEmpty(config.Title))
                        {
                            title = config.Title;
                        }

                        if (!string.IsNullOrEmpty(config.Formatter))
                        {
                            try
                            {
                                var style = workbook.CreateCellStyle();

                                var dataFormat = workbook.CreateDataFormat();

                                style.DataFormat = dataFormat.GetFormat(config.Formatter);

                                cellStyles[i] = style;
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.ToString());
                            }
                        }

                        var titleCell = titleRow.CreateCell(index);
                        titleCell.CellStyle = titleStyle;
                        titleCell.SetCellValue(title);
                    }

                    var value = property.GetValue(item, null);
                    if (value == null)
                        continue;

                    ICell cell = row.CreateCell(index,CellType.Numeric);
                    ICellStyle cellStyle;
                    if (cellStyles.TryGetValue(i, out cellStyle))
                    {
                        cell.CellStyle = cellStyle;

                        var unwrapType = property.PropertyType.UnwrapNullableType();
                        if (unwrapType == typeof(bool))
                        {
                            cell.SetCellValue((bool)value);
                        }
                        else if (unwrapType == typeof(DateTime))
                        {
                            cell.SetCellValue(Convert.ToDateTime(value));
                        }
                        else if (unwrapType == typeof(double))
                        {
                            cell.SetCellValue(Convert.ToDouble(value));
                        }
                        else if (value is IFormattable)
                        {
                            var fv = value as IFormattable;
                            cell.SetCellValue(fv.ToString(config.Formatter, CultureInfo.CurrentCulture));
                        }
                        else
                        {
                            cell.SetCellValue(value.ToString());
                        }
                    }
                    else if (value is IFormattable)
                    {
                        var fv = value as IFormattable;
                        cell.SetCellValue(fv.ToString(config.Formatter, CultureInfo.CurrentCulture));
                    }
                    else
                    {
                        cell.SetCellValue(value.ToString());
                    }
                }

                rowIndex++;
            }

            int recordIndex = 0;

            var mergableConfigs = cellConfigs.Where(c => c != null && c.AllowMerge).ToList();
            if (mergableConfigs.Any())
            {
                var vStyle = workbook.CreateCellStyle();
                vStyle.VerticalAlignment = VerticalAlignment.Center;
                foreach (var config in mergableConfigs)
                {
                    recordIndex++;

                    object previous = null;
                    int rowspan = 0, row = 1;
                    for (row = 1; row < rowIndex; row++)
                    {
                        var value = sheet.GetRow(row).GetCellValue(config.Index);
                        if (object.Equals(previous, value) && value != null)
                        {
                            rowspan++;
                        }
                        else
                        {
                            if (rowspan > 1)
                            {
                                int cellIndex = sheet.GetRow(row - rowspan).Cells.Count - 1;
                                if (config.Index <= cellIndex)
                                {
                                    sheet.GetRow(row - rowspan).Cells[config.Index].CellStyle = vStyle;
                                    sheet.AddMergedRegion(new CellRangeAddress(row - rowspan, row - 1, config.Index, config.Index));
                                }
                            }
                            rowspan = 1;
                            previous = value;
                        }
                    }

                    if (rowspan > 1)
                    {
                        int cellIndex = sheet.GetRow(row - rowspan).Cells.Count - 1;
                        if (config.Index <= cellIndex)
                        {
                            sheet.GetRow(row - rowspan).Cells[config.Index].CellStyle = vStyle;
                            sheet.AddMergedRegion(new CellRangeAddress(row - rowspan, row - 1, config.Index, config.Index));
                        }
                    }
                }

            }

            if (rowIndex > 1)
            {
                var statistics = new List<StatisticsConfig>();
                var filterConfigs = new List<FilterConfig>();
                var freezeConfigs = new List<FreezeConfig>();
                if (fluentConfigEnabled)
                {
                    statistics.AddRange(fluentConfig.StatisticsConfigs);
                    freezeConfigs.AddRange(fluentConfig.FreezeConfigs);
                    filterConfigs.AddRange(fluentConfig.FilterConfigs);
                }
                else
                {
                    var attributes = typeof(T).GetCustomAttributes(typeof(StatisticsAttribute), true) as StatisticsAttribute[];
                    if (attributes != null && attributes.Length > 0)
                    {
                        foreach (var item in attributes)
                        {
                            statistics.Add(item.StatisticsConfig);
                        }
                    }

                    var freezes = typeof(T).GetCustomAttributes(typeof(FreezeAttribute), true) as FreezeAttribute[];
                    if (freezes != null && freezes.Length > 0)
                    {
                        foreach (var item in freezes)
                        {
                            freezeConfigs.Add(item.FreezeConfig);
                        }
                    }

                    var filters = typeof(T).GetCustomAttributes(typeof(FilterAttribute), true) as FilterAttribute[];
                    if (filters != null && filters.Length > 0)
                    {
                        foreach (var item in filters)
                        {
                            filterConfigs.Add(item.FilterConfig);
                        }
                    }
                }

                foreach (var item in statistics)
                {
                    var lastRow = sheet.CreateRow(rowIndex);
                    var cell = lastRow.CreateCell(0);
                    cell.SetCellValue(item.Name);
                    foreach (var column in item.Columns)
                    {
                        cell = lastRow.CreateCell(column);
                        cell.CellFormula = $"{item.Formula}({GetCellPosition(1, column)}:{GetCellPosition(rowIndex - 1, column)})";
                    }

                    rowIndex++;
                }

                foreach (var freeze in freezeConfigs)
                {
                    sheet.CreateFreezePane(freeze.ColSplit, freeze.RowSplit, freeze.LeftMostColumn, freeze.TopRow);
                }

                foreach (var filter in filterConfigs)
                {
                    sheet.SetAutoFilter(new CellRangeAddress(filter.FirstRow, filter.LastRow ?? rowIndex, filter.FirstCol, filter.LastCol));
                }
            }

            for (int i = 0; i < properties.Length; i++)
            {
                //sheet.AutoSizeColumn(i);
            }

            return workbook;
        }

        private static IWorkbook InitializeWorkbook(string excelFile)
        {
            var setting = ExcelHelper.Setting;
            if (setting.UserXlsx)
            {
                if (!string.IsNullOrEmpty(excelFile) && File.Exists(excelFile))
                {
                    using (var file = new FileStream(excelFile, FileMode.Open, FileAccess.Read))
                    {
                        return new HSSFWorkbook(file);
                    }
                }
                else
                {
                    return new HSSFWorkbook();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(excelFile) && File.Exists(excelFile))
                {
                    using (var file = new FileStream(excelFile, FileMode.Open, FileAccess.Read))
                    {
                        return new HSSFWorkbook(file);
                    }
                }
                else
                {
                    var hssf = new HSSFWorkbook();

                    var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    hssf.DocumentSummaryInformation = dsi;

                    var si = PropertySetFactory.CreateSummaryInformation();
                    hssf.SummaryInformation = si;

                    return hssf;
                }
            }
        }

        private static string GetCellPosition(int row, int col)
        {
            col = Convert.ToInt32('A') + col;
            row = row + 1;
            return ((char)col) + row.ToString();
        }
    }
}
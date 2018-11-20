using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Jwell.Framework.Excel
{
    public delegate object ValueConverter(int row, int cell, object value);

    public static class ExcelHelper
    {
        public static ExcelSetting Setting { get; set; } = new ExcelSetting();

        /// <summary>
        /// 加载Excel文件数据到IEnumerable <see cref="IEnumerable{T}"/> 
        /// /// </summary>
        public static IEnumerable<T> Load<T>(string excelFile, Stream stream = null, int startRow = 1, int sheetIndex = 0, ValueConverter valueConverter = null) where T : class, new()
        {
            if (stream == null && !File.Exists(excelFile))
            {
                throw new FileNotFoundException();
            }

            var workbook = stream != null ? InitializeWorkbook(stream, excelFile) : InitializeWorkbook(excelFile);

            var sheet = workbook.GetSheetAt(sheetIndex);

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

            bool fluentConfigEnabled = false;

            IFluentConfiguration fluentConfig;

            if (Setting.FluentConfigs.TryGetValue(typeof(T), out fluentConfig))
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

            var statistics = new List<StatisticsConfig>();
            if (fluentConfigEnabled)
            {
                statistics.AddRange(fluentConfig.StatisticsConfigs);
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
            }

            var list = new List<T>();
            int idx = 0;

            IRow headerRow = null;

            var rows = sheet.GetRowEnumerator();
            while (rows.MoveNext())
            {
                var row = rows.Current as IRow;

                if (idx == 0)
                    headerRow = row;
                idx++;

                if (row.RowNum < startRow)
                {
                    continue;
                }

                var item = new T();
                var itemIsValid = true;
                for (int i = 0; i < properties.Length; i++)
                {
                    var prop = properties[i];

                    int index = i;
                    var config = cellConfigs[i];
                    if (config != null)
                    {
                        if (!config.IsIgnored)
                        {
                            index = config.Index;

                            if (index < 0 && config.AutoIndex && !string.IsNullOrEmpty(config.Title))
                            {
                                foreach (var cell in headerRow.Cells)
                                {
                                    if (!string.IsNullOrEmpty(cell.StringCellValue))
                                    {
                                        if (cell.StringCellValue.Equals(config.Title, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            index = cell.ColumnIndex;
                                            config.Index = index;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (index < 0)
                            {
                                throw new ApplicationException("未设置Excel起始索引index，或是autoIndex");
                            }
                        }

                        var value = row.GetCellValue(index);
                        if (valueConverter != null)
                        {
                            value = valueConverter(row.RowNum, index, value);
                        }

                        if (value == null)
                        {
                            continue;
                        }

                        if (idx > startRow + 1 && index == 0 &&
                            statistics.Any(s => s.Name.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                        {
                            var st = statistics.FirstOrDefault(s => s.Name.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase));
                            var formula = row.GetCellValue(st.Columns.First()).ToString();
                            if (formula.StartsWith(st.Formula, StringComparison.InvariantCultureIgnoreCase))
                            {
                                itemIsValid = false;
                                break;
                            }
                        }

                        var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        var safeValue = Convert.ChangeType(value, propType, CultureInfo.CurrentCulture);

                        prop.SetValue(item, safeValue, null);
                    }
                }

                if (itemIsValid)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        internal static object GetCellValue(this IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null)
            {
                return null;
            }

            if (cell.IsMergedCell)
            {
                //TODO:
            }

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                    return cell.ToString();
                case CellType.Blank:
                case CellType.Unknown:
                default:
                    return null;
            }
        }

        internal static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        private static IWorkbook InitializeWorkbook(string excelFile)
        {
            if (Path.GetExtension(excelFile).Equals(".xls"))
            {
                using (var file = new FileStream(excelFile, FileMode.Open, FileAccess.Read))
                {
                    return new HSSFWorkbook(file);
                }
            }
            else if (Path.GetExtension(excelFile).Equals(".xlsx"))
            {
                using (var file = new FileStream(excelFile, FileMode.Open, FileAccess.Read))
                {
                    return new XSSFWorkbook(file);
                }
            }
            else
            {
                throw new NotSupportedException($"不存在该文件 {excelFile}");
            }
        }

        private static IWorkbook InitializeWorkbook(Stream excelStream, string excelFile)
        {
            if (excelStream != null)
            {
                if (Path.GetExtension(excelFile).Equals(".xls"))
                {
                    return new HSSFWorkbook(excelStream);
                }
                else
                {
                    return new XSSFWorkbook(excelStream);
                }
            }
            else
            {
                throw new NotSupportedException($"Excel文件流不能为空");
                
            }
        }
    }
}

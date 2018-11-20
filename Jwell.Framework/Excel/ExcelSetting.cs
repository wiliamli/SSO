using System;
using System.Collections.Generic;

namespace Jwell.Framework.Excel
{
    public class ExcelSetting
    {
        public bool UserXlsx { get; set; } = false;

        public string DateFormatter { get; set; } = "yyyy-MM-dd HH:mm:ss";

        public FluentConfiguration<TModel> For<TModel>() where TModel : class
        {
            var mc = new FluentConfiguration<TModel>();

            FluentConfigs[typeof(TModel)] = mc;

            return mc;
        }

        internal IDictionary<Type, IFluentConfiguration> FluentConfigs { get; } = new Dictionary<Type, IFluentConfiguration>();
    }
}

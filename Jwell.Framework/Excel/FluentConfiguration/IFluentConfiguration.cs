using System.Collections.Generic;
using System.Reflection;

namespace Jwell.Framework.Excel
{
    internal interface IFluentConfiguration
    {
     
        IDictionary<PropertyInfo, PropertyConfiguration> PropertyConfigs { get; }

  
        IList<StatisticsConfig> StatisticsConfigs { get; }

     
        IList<FilterConfig> FilterConfigs { get; }

        IList<FreezeConfig> FreezeConfigs { get; }
    }
}

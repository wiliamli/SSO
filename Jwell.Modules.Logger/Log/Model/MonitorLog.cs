using Jwell.Framework.Utilities;
using Jwell.Modules.Logger.Log.Constant;
using Newtonsoft.Json;
using System;

namespace Jwell.Modules.Logger.Log.Model
{
    /// <summary>
    /// 监控日志
    /// </summary>
    internal class MonitorLog : LogBase
    {
        internal MonitorLog()
        { }

        internal MonitorLog(Marker marker)
        {
            Module = marker.Module;
            Category = marker.Category;
            SubCategory = marker.SubCategory;
        }

        [JsonProperty("Version")]
        internal string Version => SetupConfig.SetupConfig.Version;

        [JsonProperty("ConfigPath")]
        internal string ConfigPath => SetupConfig.SetupConfig.ConfigPath;

        [JsonProperty("Environment")]
        internal string Environment => SetupConfig.SetupConfig.Enviroment;

        [JsonProperty("QueueSizeCounter")]
        internal long QueueSizeCounter => TransportFactory.Channel.QueueSizeCounter;

        [JsonProperty("EnqueueCounter")]
        internal long EnqueueCounter => TransportFactory.Channel.EnqueueCounter;

        [JsonProperty("DequeueCounter")]
        internal long DequeueCounter => TransportFactory.Channel.DequeueCounter;

        [JsonProperty("OverflowCounter")]
        internal long OverflowCounter => TransportFactory.Channel.OverflowCounter;

        [JsonProperty("MaxSize")]
        internal int MaxSize
        {
            get
            {
                return TransportFactory.Channel.MaxSize;
            }
            set
            {
                TransportFactory.Channel.MaxSize = value;
            }
        }

        [JsonProperty("IP")]
        internal string IP => SetupConfig.SetupConfig.IP;


        private string module = SetupConfig.SetupConfig.Module;
        [JsonProperty("Module")]
        internal string Module
        {
            get { return module; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    module = value;
            }
        }

        private string category = SetupConfig.SetupConfig.Category;
        [JsonProperty("Category")]
        internal string Category
        {
            get { return category; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    category = value;
            }
        }

        private string subCategory = SetupConfig.SetupConfig.SubCategory;
        [JsonProperty("SubCategory")]
        internal string SubCategory
        {
            get { return subCategory; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    subCategory = value;
            }
        }

        private string filterOne = LogConstant.DEFAULTVALUE;
        [JsonProperty("FilterOne")]
        internal string FilterOne
        {
            get
            {
                return filterOne;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    filterOne = value;
            }
        }

        private string filterTwo = LogConstant.DEFAULTVALUE;
        [JsonProperty("FilterTwo")]
        internal string FilterTwo
        {
            get
            {
                return filterTwo;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    filterTwo = value;
            }
        }

        [JsonProperty("ServiceNumber")]
        internal string ServiceNumber => SetupConfig.SetupConfig.ServiceNumber;

        [JsonProperty("ServiceSign")]
        internal string ServiceSign => SetupConfig.SetupConfig.ServiceSign;

        [JsonProperty("StartTime")]
        internal DateTime StartTime => DateTime.Now;

        [JsonProperty("TraceID")]
        internal string TraceID => $"Heart_Monitor_{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";

        internal string AsJsonString()
        {
            //可以写入MongoDB
            return Serializer.ToJson(this);
        }
    }
}

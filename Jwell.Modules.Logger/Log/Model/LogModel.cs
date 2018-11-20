using Jwell.Modules.Logger.Log.Constant;
using Newtonsoft.Json;
using System;

namespace Jwell.Modules.Logger.Log.Model
{
    internal class LogModel: LogBase
    {
        [JsonProperty("ServiceNumber")]
        internal string ServiceNumber { get; set; }

        [JsonProperty("ServiceSign")]
        internal string ServiceSign { get; set; }

        private string traceID = LogConstant.DEFAULTVALUE;
        [JsonProperty("TraceID")]
        internal string TraceID
        {
            get { return traceID; }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    traceID = value;
                }
            }
        }

        private string message = LogConstant.DEFAULTVALUE;
        [JsonProperty("Message")]
        internal string Message
        {
            get
            {
                return message;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    message = value;
                }
            }
        }

        private string stackTrace = LogConstant.DEFAULTVALUE;
        [JsonProperty("StackTrace")]
        internal string StackTrace
        {
            get
            {
                return stackTrace;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    stackTrace = value;
                }
            }
        }


        private string filterOne = LogConstant.DEFAULTVALUE;
        [JsonProperty("FilterOne")]
        internal string Filter1
        {
            get
            {
                return filterOne;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    filterOne = value;
                }
            }
        }


        private string filterTwo = LogConstant.DEFAULTVALUE;
        [JsonProperty("FilterTwo")]
        internal string Filter2 {
            get
            {
                return filterTwo;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    filterTwo = value;
                }
            }
        }

        [JsonProperty("RecordTime")]
        internal DateTime RecordTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        [JsonProperty("LogType")]
        internal byte LogType { get; set; }

        private string environment = LogConstant.DEFAULTVALUE;
        [JsonProperty("Environment")]
        internal string Environment {
            get
            {
                return environment;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    environment = value;
                }
            }
        }

        private string ip = LogConstant.DEFAULTVALUE;
        [JsonProperty("IP")]
        internal string IP {
            get
            {
                return ip;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    ip = value;
                }
            }
        }

        private string category = LogConstant.DEFAULTVALUE;
        [JsonProperty("Category")]
        internal string Category {
            get
            {
                return category;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    category = value;
                }
            }
        }

        private string subCategory = LogConstant.DEFAULTVALUE;
        [JsonProperty("SubCategory")]
        internal string SubCategory {
            get
            {
                return subCategory;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    subCategory = value;
                }
            }
        }

        private string domainName = LogConstant.DEFAULTVALUE;
        [JsonProperty("DomainName")]
        internal string DomainName {
            get
            {
                return domainName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    domainName = value;
                }
            }
        }
    }
}

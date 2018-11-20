using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Framework.Settings
{
    public sealed class GlobalSettings
    {
        private static GlobalSettings _instance;

        public static GlobalSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("Cannot get the instance of GlobalSettings, it hasn't been initialized");
                }

                return _instance;
            }
        }

        internal static void Initialize()
        {
            _instance = new GlobalSettings();
        }

        private static ConcurrentDictionary<Type, object> _settings = new ConcurrentDictionary<Type, object>();

        private GlobalSettings()
        {
            Set(Ioc.IocSettings.Default);
        }

        /// <summary>
        /// Set setting value
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <param name="value"></param>
        public void Set<TSetting>(TSetting value)
        {
            Type key = typeof(TSetting);

            _settings[key] = value;
        }

        /// <summary>
        /// Get setting value
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        public TSetting Get<TSetting>()
        {
            Type key = typeof(TSetting);

            object value;

            if (_settings.TryGetValue(key, out value))
            {
                return (TSetting)value;
            }

            return default(TSetting);
        }

        /// <summary>
        /// Check if contains the setting value
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        public bool Contains<TSetting>()
        {
            Type key = typeof(TSetting);

            return _settings.ContainsKey(key);
        }
    }
}

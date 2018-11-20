using Jwell.Framework.Ioc;

namespace Jwell.Framework.Settings
{
    public static class GlobalSettingsExtensions
    {
        /// <summary>
        /// Retrive the IoC settings
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IocSettings IocSettings(this GlobalSettings settings)
        {
            return settings.Get<IocSettings>();
        }
    }
}

namespace Jwell.Framework.Ioc
{
    public class SingletonAttribute : ComponentAttribute
    {
        public SingletonAttribute() : base(ServiceLifetime.Singleton)
        {

        }
    }
}

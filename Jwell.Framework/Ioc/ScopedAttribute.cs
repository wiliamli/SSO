namespace Jwell.Framework.Ioc
{
    public class ScopedAttribute : ComponentAttribute
    {
        public ScopedAttribute() : base(ServiceLifetime.Scoped)
        {
        }
    }
}

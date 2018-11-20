namespace Jwell.Framework.Ioc
{
    public class TransientAttribute : ComponentAttribute
    {
        public TransientAttribute() : base(ServiceLifetime.Transient)
        {

        }
    }
}

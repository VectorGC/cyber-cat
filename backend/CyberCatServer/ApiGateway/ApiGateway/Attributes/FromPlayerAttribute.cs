using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromPlayerAttribute : ModelBinderAttribute
    {
        public FromPlayerAttribute() : base(typeof(PlayerIdBinder))
        {
        }
    }
}
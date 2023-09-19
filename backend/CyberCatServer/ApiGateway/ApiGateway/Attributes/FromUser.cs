using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromUserAttribute : ModelBinderAttribute
    {
        public FromUserAttribute() : base(typeof(UserIdBinder))
        {
        }
    }
}
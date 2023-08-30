using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromUser : ModelBinderAttribute
    {
        public FromUser() : base(typeof(UserIdBinder))
        {
        }
    }

    /*
    public class GetUserIdConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            foreach (var parameter in action.Parameters)
            {
                parameter.BindingInfo ??= new BindingInfo();
                parameter.BindingInfo.BinderType = typeof(MyBinder);
            }
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var value = Convert.ChangeType(valueProviderResult.FirstValue, bindingContext.ModelType);
                bindingContext.Result = ModelBindingResult.Success(value);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid value");
            }

            return Task.CompletedTask;
        }
    }
    
    public class UserIdBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ElementMetadata.GetA .Metadata.ParameterInfo == null)
                return null;

            var attribute = context.Metadata.ParameterInfo.GetCustomAttribute<CustomFromAttribute>();
            if (attribute == null)
                return null;

            return new CustomFromBinder();
        }
    }

    public class AuthorizeTokenGuard : IAsyncResourceFilter
    {
        private readonly IAuthGrpcService _authGrpcService;
        private readonly ILogger _logger;

        public AuthorizeTokenGuard(IAuthGrpcService authGrpcService, ILogger<AuthorizeTokenGuard> logger)
        {
            _authGrpcService = authGrpcService;
            _logger = logger;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userId = await context.HttpContext.User.GetUserId(_authGrpcService);
            if (userId == null)
            {
                context.Result = new ForbidResult(JwtBearerDefaults.AuthenticationScheme);
                return;
            }

            var token = context.HttpContext.Request.Query["token"];
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedObjectResult("token not found");
                    return;
                }

                var userId = await _authUserService.Authorize(token);
                context.HttpContext.Items[typeof(UserId)] = userId;
            }
            catch (UserNotFound)
            {
                _logger.LogInformation("token '{Token}' is invalid", token);
                context.Result = new UnauthorizedObjectResult("token is invalid");
            }
        }
    }
    */
}
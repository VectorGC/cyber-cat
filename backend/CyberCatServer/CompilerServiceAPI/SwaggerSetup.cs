using System.Reflection;

namespace CompilerServiceAPI;

public static class SwaggerSetup
{
    public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSwaggerGen(options =>
        {
            // Подтягиваем в swagger xml комментарии методов.
            // https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    public static IApplicationBuilder UseSwaggerSwashbuckle(this IApplicationBuilder app)
    {
        // Показываем API спецификацию через swagger.
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    public static void FallbackToSwaggerPage(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapFallback((context) =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            }
        );
    }
}
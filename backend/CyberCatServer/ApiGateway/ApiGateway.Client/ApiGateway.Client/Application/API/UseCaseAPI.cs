using ApiGateway.Client.Application.UseCases;

namespace ApiGateway.Client.Application.API
{
    public abstract class UseCaseAPI
    {
        private readonly TinyIoCContainer _container;

        public UseCaseAPI(TinyIoCContainer container)
        {
            _container = container;
        }

        public TUseCase GetUseCase<TUseCase>() where TUseCase : class, IUseCase
        {
            return _container.Resolve<TUseCase>();
        }
    }
}
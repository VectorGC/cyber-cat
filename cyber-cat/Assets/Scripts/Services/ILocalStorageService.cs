using Models;

namespace Services
{
    public interface ILocalStorageService
    {
        IPlayer Player { get; set; }
    }
}
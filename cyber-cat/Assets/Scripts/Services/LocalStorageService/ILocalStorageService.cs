using JetBrains.Annotations;
using Models;

namespace Services
{
    public interface ILocalStorageService
    {
        [CanBeNull] IPlayer Player { get; set; }
        void RemoveAll();
    }
}
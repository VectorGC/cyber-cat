using Models;

namespace RestAPI.InternalDto
{
    internal class PlayerDto : IPlayer
    {
        public string Name { get; set; }
        public ITokenSession Token { get; set; }
    }
}
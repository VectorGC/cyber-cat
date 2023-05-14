using Models;

namespace ServerAPI.InternalDto
{
    internal class PlayerDto : IPlayer
    {
        public string Name { get; set; }
        public ITokenSession Token { get; set; }
    }
}
using Models;

namespace Services.InternalModels
{
    internal class Player : IPlayer
    {
        public string Name { get; }

        public ITokenSession Token { get; }

        public Player(string name, ITokenSession token)
        {
            Name = name;
            Token = token;
        }
    }
}
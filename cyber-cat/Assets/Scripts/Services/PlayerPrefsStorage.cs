using Models;
using Services.InternalModels;
using UnityEngine;

namespace Services
{
    public class PlayerPrefsStorage : ILocalStorageService
    {
        public IPlayer Player
        {
            get => GetPlayer();
            set => SavePlayer(value);
        }

        public void RemoveAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        private IPlayer GetPlayer()
        {
            var playerName = PlayerPrefs.GetString("player_name");
            var tokenValue = PlayerPrefs.GetString("token");

            var token = new TokenSession(tokenValue);

            return new Player(playerName, token);
        }

        private void SavePlayer(IPlayer player)
        {
            var playerName = player.Name;
            var tokenValue = player.Token.Value;

            PlayerPrefs.SetString("player_name", playerName);
            PlayerPrefs.SetString("token", tokenValue);

            PlayerPrefs.Save();
        }
    }
}
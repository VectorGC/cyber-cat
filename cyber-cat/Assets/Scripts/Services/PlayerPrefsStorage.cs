using Models;
using Services.InternalModels;
using UnityEngine;

namespace Services
{
    public class PlayerPrefsStorage : ILocalStorageService
    {
        private const string PlayerNamePrefsKey = "player_name";
        private const string TokenPrefsKey = "token";

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
            var playerName = PlayerPrefs.GetString(PlayerNamePrefsKey);
            var tokenValue = PlayerPrefs.GetString(TokenPrefsKey);

            var token = new TokenSession(tokenValue);

            return new Player(playerName, token);
        }

        private void SavePlayer(IPlayer player)
        {
            var playerName = player.Name;
            var tokenValue = player.Token.Value;

            PlayerPrefs.SetString(PlayerNamePrefsKey, playerName);
            PlayerPrefs.SetString(TokenPrefsKey, tokenValue);

            PlayerPrefs.Save();
        }
    }
}
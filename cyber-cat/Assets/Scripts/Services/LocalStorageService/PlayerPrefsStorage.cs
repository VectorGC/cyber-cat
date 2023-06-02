using JetBrains.Annotations;
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
            PlayerPrefs.DeleteKey(PlayerNamePrefsKey);
            PlayerPrefs.DeleteKey(TokenPrefsKey);
            PlayerPrefs.Save();
        }

        [CanBeNull]
        private IPlayer GetPlayer()
        {
            var tokenValue = PlayerPrefs.GetString(TokenPrefsKey);
            if (string.IsNullOrEmpty(tokenValue))
            {
                return null;
            }

            var playerName = PlayerPrefs.GetString(PlayerNamePrefsKey);

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
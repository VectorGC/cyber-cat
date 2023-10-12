using System;

namespace Assets.SimpleVKSignIn.Scripts
{
    [Serializable]
    public class UserInfo
    {
        public long id;
        public string first_name;
        public string last_name;
        public bool can_access_closed;
        public bool is_closed;

        /// <summary>
        /// Extra fields: https://dev.vk.com/ru/method/users.get
        /// </summary>
        public int has_photo;
        public string photo_200;
    }
}
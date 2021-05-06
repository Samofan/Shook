using System;
using Server.Models;

namespace Model.Login
{
    /// <summary>
    /// A login package is returned by a login controller by the REST api. Depending
    /// on the success flag the LoginPackage contains the User who logged in and
    /// the  api key.
    /// </summary>
    public class LoginPackage
    {
        /// <summary>
        /// The success flag is set if the username and password are correct.
        /// </summary>
        private bool _success { get; set; }

        /// <summary>
        /// This field contains the user who logged in. Applications can fill
        /// their account page with these information.
        /// </summary>
        private UserDto _accountUser { get; set; }

        /// <summary>
        /// The api key is used to authenticate at the REST api.
        /// </summary>
        private string _apiKey { get; set; }

        /// <summary>
        /// This property defines until an api key is valid. 
        /// </summary>
        private DateTime _validUntil { get; set; }

        /// <summary>
        /// Property of _success.
        /// </summary>
        public bool Success
        {
            get => _success;
            set => _success = value;
        }

        /// <summary>
        /// Property of _accountUser.
        /// </summary>
        public UserDto AccountUser
        {
            get => _accountUser;
            set => _accountUser = value;
        }

        /// <summary>
        /// Property of _apiKey.
        /// </summary>
        public string ApiKey
        {
            get => _apiKey;
            set => _apiKey = value;
        }

        /// <summary>
        /// Property of _validUntil.
        /// </summary>
        public DateTime ValidUntil
        {
            get => _validUntil;
            set => _validUntil = value;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoginPackage()
        {
        }
    }
}

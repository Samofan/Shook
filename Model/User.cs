using Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User
    {
        private string _username;

        private string _password;

        private string _email;

        private Uri _profilePicture;

        private List<Shook> _shooks;

        public string Username 
        { 
            get => _username; 
            set => _username = value; 
        }

        public string Password 
        { 
            get => _password;
            set => _password = value; 
        }

        public string Email 
        { 
            get => _email;
            set => _email = value; 
        }

        public Uri ProfilePicture 
        { 
            get => _profilePicture; 
            set => _profilePicture = value; 
        }

        public List<Shook> Shooks
        {
            get => _shooks;
            set => _shooks = value;
        }

        public User(UserDto userDto)
        {
            _username = userDto.Username;
            _password = userDto.Password;
            _email = userDto.Email;
            _profilePicture = userDto.ProfilePicture;
            _shooks = SetShookList(userDto);
        }

        public User()
        {
        }

        private List<Shook> SetShookList(UserDto userDto)
        {
            return new List<Shook>();
        }
    }
}

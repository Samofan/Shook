using System;
using System.Collections.Generic;
using Model;

namespace Server.Models
{

    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Uri ProfilePicture { get; set; }

        public List<UserShookDto> UserShooks { get; set; } = new List<UserShookDto>();
    }
}

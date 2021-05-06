using Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class UserShookDto
    {
        public int ShookDtoId { get; set; }

        public ShookDto Shook { get; set; }

        public int UserDtoId { get; set; }

        public UserDto User {get; set;}
    }
}

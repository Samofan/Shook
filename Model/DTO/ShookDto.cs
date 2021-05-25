using System;
using System.Collections.Generic;
using Server.Models;

namespace Model
{
    public class ShookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public UserDto Creator { get; set; }

        public UserDto Winner { get; set; }

        public List<UserShookDto> ShookUsers { get; set; } = new List<UserShookDto>();

        public ShookDto()
        {

        }
    }
}

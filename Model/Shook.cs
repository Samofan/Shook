using System;
using System.Collections.Generic;
using Server.Models;

namespace Model
{
    public class Shook
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<User> Member { get; set; }

        public int CreatorId { get; set; }

        public int WinnerId { get; set; }

        public Shook()
        {
            Member = new List<User>();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }
    }
}

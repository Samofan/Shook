using System;
using System.Collections.Generic;
using Model;

namespace Server.Models
{

    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Uri ProfilePicture { get; set; }

        public virtual ICollection<Shook> Shooks { get; set; }

        /*public virtual ICollection<Shook> CreatedShooks
        {
            get
            {
                var createdList = new List<Shook>();

                foreach (Shook shook in Shooks)
                {
                    if (shook.Creator == this)
                    {
                        createdList.Add(shook);
                    }
                }

                return createdList;
            }
        }

        public virtual ICollection<Shook> WonShooks
        {
            get
            {
                var wonShooks = new List<Shook>();

                foreach (Shook shook in Shooks)
                {
                    if (shook.Winner.Id == this.Id)
                    {
                        wonShooks.Add(shook);
                    }
                }

                return wonShooks;
            }
        }*/

        public User()
        {
            Shooks = new List<Shook>();
        }

        public void CreateShook()
        {
            throw new NotImplementedException();
        }

        public void EditShook()
        {
            throw new NotImplementedException();
        }

        public void JoinShook(Shook shook)
        {
            throw new NotImplementedException();
        }

        public void LeaveShook(Shook shook)
        {
            throw new NotImplementedException();
        }
    }
}

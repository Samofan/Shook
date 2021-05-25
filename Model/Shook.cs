using System;
using System.Collections.Generic;

namespace Model
{
    public class Shook
    {
        private int _id;

        private string _title;

        private string _description;

        private DateTime _startTime;

        private DateTime _endTime;

        private User _creator;

        private User _winner;

        private List<User> _members;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Title 
        {
            get => _title;
            set => _title = value; 
        }

        public string Description 
        { 
            get => _description;
            set => _description = value; 
        }

        public DateTime StartTime 
        { 
            get => _startTime; 
            set => _startTime = value; 
        }

        public DateTime EndTime 
        { 
            get => _endTime; 
            set => _endTime = value; 
        }

        public User Creator 
        { 
            get => _creator; 
            set => _creator = value; 
        }

        public User Winner 
        { 
            get => _winner;
            set => _winner = value; 
        }

        public Shook(ShookDto shookDto)
        {
            _title = shookDto.Title;
            _description = shookDto.Description;
            _startTime = shookDto.StartTime;
            _endTime = shookDto.EndTime;
            // TODO: Set creator and winner.
            _creator = new User();
            _winner = new User();
            _members = SetMemberList(shookDto);
        }
        
        public Shook()
        {

        }

        private List<User> SetMemberList(ShookDto shookDto)
        {
            return new List<User>();
        }
    }
}

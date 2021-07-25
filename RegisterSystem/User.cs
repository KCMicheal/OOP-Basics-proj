using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration
{
    class User
    {
        private bool _displayAge = true;
        private int? _age;
        public User(DateTime birthday)
        {
            Birthday = birthday;
            Age = DateTime.Now.Year - Birthday.Year;
        }



        public string Fullname { get; set; }
        public string Email { get; set; }

        public int? Age
        {
            get { return _displayAge ? _age : null; }
            set { _age = value.Value; }
        }


        public DateTime Birthday { get; }

        public Gender Gender { get; set; }

        public string Password { get; set; }

        public DateTime Created { get; } = DateTime.Now;

        public bool ToggleAgePrivacy()
        {
            _displayAge = !_displayAge;
            return _displayAge;
        }
    }

    public enum Gender
    {
        SelectGender,
        Male = 1,
        Female,
        PreferNotToSay
    }
}

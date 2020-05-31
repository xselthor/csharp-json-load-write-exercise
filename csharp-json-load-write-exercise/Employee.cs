using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_json_load_write_exercise
{
    class Employee : IPerson
    {
        private Guid ID { get; set; }
        public string email { get; set; }
        public string title { get; set; }
        public string position { get; set; }
        public string city { get; set; }
        public string building { get; set; }
        public string room { get; set; }
        public decimal salary { get; set; }
        public decimal rise { get; set; }
        public string DateOfemployment { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string DateOfBirth { get; set; }
        public bool Spouse { get; set; }
        public DateTime DateAdded { get; set; }
        public string Phone { get; set; }

        public Employee()
        {
            ID = Guid.NewGuid();
            DateAdded = DateTime.Now;
        }
    }
}

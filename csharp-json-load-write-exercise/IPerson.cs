using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_json_load_write_exercise
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Nickname { get; set; }
        string DateOfBirth { get; set; }
        bool Spouse { get; set; }
        DateTime DateAdded { get; set; }
        string Phone { get; set; }
    }
}

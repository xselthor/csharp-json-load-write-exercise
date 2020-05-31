using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_json_load_write_exercise
{
    class Validation
    {
        public bool IsValidEmail(string email)
        {
            try
            {
                email = email.Trim();
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsDateTime(string tempDate)
        {
            DateTime fromDateValue;
            string format = "yyyy/MM/dd";
            return DateTime.TryParseExact(tempDate, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue);
        }
    }
}

using System;
using System.Linq;

namespace Selenium_Testing
{
    public static class Helpers
    {
        public static string baseUrl = "https://contactbook.evgenidimitrov0.repl.co/";
        
        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "abcdefjhijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GetRandomNumber()
        {
            return random.Next(1000, 9999);
        }

        public static ContactData FillContact(string fname, string lname, string email, string phone, string comment)
        {
            return new ContactData()
            {
                FirstName = fname,
                LastName = lname,
                Email = email,
                Phone = phone,
                Comments = comment
            };
        }

    }
}

using System;
using System.Linq;

namespace API_Testing
{
    public static class Helpers
    {
        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "abcdefjhijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static int GetRandomNumber()
        {
            return random.Next(1000, 9999);
        }

    }
}

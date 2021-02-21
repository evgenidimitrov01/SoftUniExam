using System;
using System.Linq;

namespace Selenium_Testing
{
    public static class Helpers
    {
        public static string baseUrl = "https://contactbook.nakov.repl.co/";
        
        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "abcdefjhijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

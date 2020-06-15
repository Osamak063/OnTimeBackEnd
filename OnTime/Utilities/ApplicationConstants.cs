using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Utilities
{
    public class ApplicationConstants
    {
        public const int PASSWORD_STRING_LENGTH = 5;
        public const string USER_NAME_PREFIX = "Otcs";

        public static string PasswordString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}

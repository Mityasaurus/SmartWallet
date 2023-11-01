using System.Text.RegularExpressions;

namespace SmartWallet.Aplication.Services
{
    public static class DataValidatorService
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+\d{10,}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
        }
    }
}

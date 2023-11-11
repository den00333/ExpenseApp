using System;
using System.IO;

namespace ExpenseApp
{
    internal class OTPManager
    {
        private const string FilePath = "otp.txt";
        public static Tuple<string, DateTime> LoadOTP()
        {
            try
            {
                string[] lines = File.ReadAllLines(FilePath);
                if (lines.Length == 2 && DateTime.TryParse(lines[1], out DateTime expirationTime))
                {
                    return new Tuple<string, DateTime>(lines[0], expirationTime);
                }
            }
            catch (Exception)
            {
                // Handle file reading errors
            }

            return null;
        }

        public static void SaveOTP(string otp, DateTime expirationTime)
        {
            try
            {
                File.WriteAllLines(FilePath, new[] { otp, expirationTime.ToString() });
            }
            catch (Exception)
            {
                // Handle file writing errors
            }
        }
    }
}
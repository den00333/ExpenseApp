using System;
using System.IO;

namespace ExpenseApp
{
    internal class OTPManager
    {
        private const string FilePath = "otp.txt";
        public static Tuple<string, DateTime> LoadOTP()
        {
            try{
                if(File.Exists(FilePath)){
                    string[] lines = File.ReadAllLines(FilePath);
                    if (lines.Length == 2 && DateTime.TryParse(lines[1], out DateTime expirationTime)){
                        return new Tuple<string, DateTime>(lines[0], expirationTime);
                    }
                }
            }
            catch (Exception){
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
            }
        }
        public static void ClearOTP()
        {
            try{
                File.WriteAllText(FilePath, string.Empty);
            }
            catch (Exception){
            }
        }
    }
}
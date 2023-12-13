using Google.Cloud.Firestore;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace ExpenseApp
{
    internal class OTPManager
    {
        private const string FilePath = "otp.txt";
        static private DateTime currentDate = DateTime.Now;
        static string OTPDate = "000000" + Environment.NewLine + currentDate.ToString() + Environment.NewLine + "bautistamacmac331@gmail.com" + Environment.NewLine;
        public static Tuple<string, DateTime,string> LoadOTP()
        {
            try{
                if(File.Exists(FilePath)){
                    string[] lines = File.ReadAllLines(FilePath);
                    if (lines.Length == 3 && DateTime.TryParse(lines[1], out DateTime expirationTime)){
                        return new Tuple<string, DateTime, string>(lines[0], expirationTime, lines[2]);
                    }
                }
            }
            catch (Exception){
            }

            return null;
        }

        public static void SaveOTP(string otp, DateTime expirationTime, string email)
        {
            try
            {
                File.WriteAllLines(FilePath, new[] { otp, expirationTime.ToString(), email });
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
        public static void createFile()
        {
            try
            {
                File.WriteAllText(FilePath, OTPDate);
                Console.WriteLine($"File created successfully at: {FilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating file: {ex.Message}");
            }
        }
    }
}

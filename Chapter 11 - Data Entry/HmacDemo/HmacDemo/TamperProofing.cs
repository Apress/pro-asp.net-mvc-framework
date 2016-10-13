using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace HmacDemo
{
    public static class TamperProofing
    {
        // For your app, change the Key to any 8 random bytes, and keep it secret
        static byte[] Key = new byte[] { 93, 101, 2, 239, 55, 0, 16, 188 };
        public enum HMACResult { OK, Expired, Invalid }

        public static string GetExpiringHMAC(string message, DateTime expiryDate)
        {
            HMAC alg = new HMACSHA1(Key);
            try
            {
                string input = expiryDate.Ticks + message;
                byte[] hashBytes = alg.ComputeHash(Encoding.UTF8.GetBytes(input));
                byte[] result = new byte[8 + hashBytes.Length];
                hashBytes.CopyTo(result, 8);
                BitConverter.GetBytes(expiryDate.Ticks).CopyTo(result, 0);
                return Swap(Convert.ToBase64String(result), "+=/", "-_,");
            }
            finally { alg.Clear(); }
        }

        public static HMACResult Verify(string message, string expiringHMAC)
        {
            byte[] bytes = Convert.FromBase64String(Swap(expiringHMAC, "-_,", "+=/"));
            DateTime claimedExpiry = new DateTime(BitConverter.ToInt64(bytes, 0));
            if (claimedExpiry < DateTime.Now)
                return HMACResult.Expired;
            else if (expiringHMAC == GetExpiringHMAC(message, claimedExpiry))
                return HMACResult.OK;
            else
                return HMACResult.Invalid;
        }

        private static string Swap(string str, string input, string output)
        {
            // Used to avoid any characters that aren't safe in URLs
            for (int i = 0; i < input.Length; i++)
                str = str.Replace(input[i], output[i]);
            return str;
        }
    }
}
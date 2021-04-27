using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace TCE_DOMAIN
{
    public static class Helper
    {
        #region Encrypt/Decrypt

        public static string Dec(this string vlr)
        {
            if (vlr.Length <= 4) return vlr.Replace("\"", "");

            return CryptoCalc(vlr.Replace("\"", ""), "D0", false);
        }

        public static string Enc(this string vlr)
        {
            if (vlr.Length <= 0) return vlr;
            var a = vlr;
            var c = CryptoCalc(a, "X2", true);
            var r = c;
            return r;
        }

        private static string CryptoCalc(string vlr, string type, bool IsEncrypt)
        {
            if (vlr.Length <= 0) return vlr;

            var result = "";
            if (IsEncrypt)
                result = Encrypt(vlr, true);
            else
                result = Decrypt(vlr, true);

            return result;
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

            string key = "*Lhfas729}f72!@3&3.!78asLHjfa";
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            
            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();

            string key = "*Lhfas729}f72!@3&3.!78asLHjfa";

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        #endregion

    }
}

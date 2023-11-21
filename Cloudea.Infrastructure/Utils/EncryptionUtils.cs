using System;
using System.Security.Cryptography;
using System.Text;

namespace Cloudea.Infrastructure.Utils
{
    public static class EncryptionUtils
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncryptMD5(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("text is null or empty");
            }
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                var strResult = BitConverter.ToString(result);
                return strResult;
            }
        }

        /// <summary>
        /// HMAC SHA 256 加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string HMACSHA256(string message, string secret)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("message is null or empty");
            }
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("secret is null or empty");
            }
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }


        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEncrypt(string encryptStr,string key)
        {
            if (string.IsNullOrEmpty(encryptStr)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(encryptStr);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecrypt(string encryptStr,string key)
        {
            if (string.IsNullOrEmpty(encryptStr)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(encryptStr);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="inputdata">输入的数据encryptedData</param>  
        /// <param name="AesKey">key</param>  
        /// <param name="AesIV">向量128</param>  
        /// <returns name="result">解密后的字符串</returns>  
        public static string AESDecrypt(string inputdata, string AesKey, string AesIV)
        {
            try
            {
                byte[] encryptedData = Convert.FromBase64String(inputdata);

                RijndaelManaged rijndaelCipher = new RijndaelManaged
                {
                    Key = Convert.FromBase64String(AesKey), // Encoding.UTF8.GetBytes(AesKey);  
                    IV = Convert.FromBase64String(AesIV),// Encoding.UTF8.GetBytes(AesIV);  
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                string result = Encoding.UTF8.GetString(plainText);

                return result;
            }
            catch (Exception)
            {
                return null;

            }
        }


    }
}
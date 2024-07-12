using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ClinicManagementSystem.Security
{
    public class EncryptorDecryptor
    {
        public static string password = @"key1234567890";

        public static string EncryptString(string stringToEncrypt)
        {
            try
            {
                //bool returnVal = false;
                string encryptedString = string.Empty;

            //string Passphrase = password;
            string Passphrase = password;

            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();


            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;


                // Step 4. Convert the input string to a byte[]
                byte[] DataToEncrypt = UTF8.GetBytes(stringToEncrypt);

                // Step 5. Attempt to encrypt the string
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

                encryptedString = Convert.ToBase64String(Results);
                //returnVal = true;
                return encryptedString.Replace("+", "---");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
 

        }

        public static string DecryptString(string encryptedString)
        {

            try
            {
                //bool returnVal = false;
                string originalString = string.Empty;

            //string Passphrase = password;
            string Passphrase = password;
            encryptedString = encryptedString.Replace("---", "+");

            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;


                // Step 4. Convert the input string to a byte[]
                byte[] DataToDecrypt = Convert.FromBase64String(encryptedString);

                // Step 5. Attempt to decrypt the string

                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);

                // Step 6. Return the decrypted string in UTF8 format
                originalString = UTF8.GetString(Results);
                //returnVal = true;
                return originalString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



            
        }
    }
}

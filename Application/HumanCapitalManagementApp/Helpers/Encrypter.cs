namespace Helpers
{
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using System.IO;
    using System.Linq;
    using Helpers.Interfaces;

    public class Encrypter : IEncrypter
    {
        private readonly string encryptionKey;

        private byte[] salt;

        public Encrypter()
        {
            this.encryptionKey = "asdafFdh23dsrfqwSasdacvasf23IsuYyjhY2";
            this.salt = Encoding.UTF8.GetBytes("Defaultna enkriptirovuchna sol himalaiska na kristali sus susiruci");
        }

        public Encrypter(string key, string salt = null)
        {
            this.encryptionKey = key;
            if (salt != null) this.salt = Encoding.UTF8.GetBytes(salt);
            else this.salt = Encoding.UTF8.GetBytes("Defaultna enkriptirovuchna sol himalaiska na kristali sus susiruci");
        }

        public string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(this.encryptionKey, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(this.encryptionKey, this.salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }
    }
}
using System.IO;
using System.Security.Cryptography;

namespace Utility
{
    public class Encripty
    {
        public static byte[] Encrypt(string FlatString, byte[] key = null, byte[] IV = null)
        {
            using (Aes AesAlgoritmo = Aes.Create())
            {
                if (key != null) AesAlgoritmo.Key = key;
                if (IV != null) AesAlgoritmo.IV = IV;
                ICryptoTransform Encryptor = AesAlgoritmo.CreateEncryptor(AesAlgoritmo.Key, AesAlgoritmo.IV);
                byte[] Encrypted;

                using (var MsEncrypt = new MemoryStream())
                {
                    using (var CsEncrypt = new CryptoStream(MsEncrypt, Encryptor, CryptoStreamMode.Write))
                    {
                        using (var SwEncrypt = new StreamWriter(CsEncrypt))
                        {
                            SwEncrypt.Write(FlatString);
                        }
                        Encrypted = MsEncrypt.ToArray();
                    }
                }
                return Encrypted;
            }
        }
    }
}

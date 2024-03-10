using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatZI
{
    class CTR
    {

        byte[] key = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        byte[] iv = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        public byte[] Encrypt(byte[] plainText)
        {
            // Check arguments.

            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create a new Aes object to perform the encryption
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                //aes.Mode = CipherMode.CTR;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Create a new memory stream to hold the encrypted data
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create a new crypto stream to perform the encryption
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Write the plaintext to the crypto stream
                        cs.Write(plainText, 0, plainText.Length);
                    }

                    // Return the encrypted data
                    encrypted = ms.ToArray();
                }
            }
            return encrypted;

        }

        public byte[] Decrypt(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            byte[] plaintext;

            using (Aes aes = Aes.Create())

            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.None;

                // duzina niza bajtova mora biti deljiva sa velicinom blokova (=16) ukoliko nije dodajemo prazne bajtove
                int blockSize = 16;
                if ((cipherText.Length % blockSize) != 0)
                {
                    int paddingLength = blockSize - (cipherText.Length % blockSize);
                    if (paddingLength == 0)
                    {
                        paddingLength = blockSize;
                    }
                    byte[] padding = new byte[paddingLength];
                    byte[] paddedPlaintext = new byte[cipherText.Length + paddingLength];
                    Buffer.BlockCopy(cipherText, 0, paddedPlaintext, 0, cipherText.Length);
                    Buffer.BlockCopy(padding, 0, paddedPlaintext, cipherText.Length, padding.Length);

                    using (var decryptor1 = aes.CreateDecryptor())
                    {
                        var plaintext1 = decryptor1.TransformFinalBlock(paddedPlaintext, 0, paddedPlaintext.Length);

                        int unpaddedLength = plaintext1.Length;
                        for (int i = plaintext1.Length - 1; i >= 0; i--)
                        {
                            if (plaintext1[i] != 0)
                            {
                                break;
                            }
                            unpaddedLength--;
                        }
                        byte[] unpaddedPlaintext = new byte[unpaddedLength];
                        Buffer.BlockCopy(plaintext1, 0, unpaddedPlaintext, 0, unpaddedLength);

                        return unpaddedPlaintext;
                    }
                }
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {

                        byte[] plaintext1 = new byte[cipherText.Length];

                        int decryptedByteCount = cs.Read(plaintext1, 0, plaintext1.Length);
                        int paddingSize = (int)plaintext1[decryptedByteCount - 1];
                        byte[] unpaddedPlainText = new byte[decryptedByteCount - paddingSize];
                        Array.Copy(plaintext1, 0, unpaddedPlainText, 0, decryptedByteCount - paddingSize);

                        return unpaddedPlainText;

                       
                    }

                   
                }
            }
        }
    }
    }


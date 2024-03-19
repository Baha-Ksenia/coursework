//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DatabaseManagement
//{
//    internal class StringEncryption
//    {
//    }
//}

using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
/// <summary>
/// Класс с методами шифрования
/// </summary>
public class StringEncryption
{
    /// <summary>
    /// Ключ шифрования
    /// </summary>
    private byte[] _key;

    public StringEncryption(string key)
    {
        if (key.Length != 32)
            throw new ArgumentException("Key must be 32 characters long (256 bits).");

        _key = Encoding.UTF8.GetBytes(key);
    }

    /// <summary>
    /// Шифрует переданную строку с использованием ключа AES и возвращает зашифрованную строку в формате base64.
    /// </summary>
    /// <param name="text">Исходная строка для шифрования.</param>
    /// <returns>Зашифрованная строка в формате base64.</returns>
    public string Encrypt(string text)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = _key; // Устанавливаем ключ шифрования
            aesAlg.IV = new byte[16]; // В реальном приложении генерируйте случайный вектор инициализации

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV); // Создаем объект для шифрования данных

            byte[] encryptedBytes;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text); // Записываем данные для шифрования
                    }
                    encryptedBytes = msEncrypt.ToArray(); // Получаем зашифрованные данные в виде массива байтов
                }
            }

            return Convert.ToBase64String(encryptedBytes); // Возвращаем зашифрованную строку в формате base64
        }
    }

    /// <summary>
    /// Расшифровывает зашифрованную строку в формате base64 с использованием ключа AES и возвращает исходную строку.
    /// </summary>
    /// <param name="text">Зашифрованная строка в формате base64.</param>
    /// <returns>Расшифрованная строка.</returns>
    public string Decrypt(string text)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = _key; // Устанавливаем ключ шифрования
            aesAlg.IV = new byte[16]; // В реальном приложении генерируйте случайный вектор инициализации

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV); // Создаем объект для расшифровки данных

            byte[] cipherBytes = Convert.FromBase64String(text); // Преобразуем зашифрованную строку из формата base64 в массив байтов
            string plaintext;
            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd(); // Читаем расшифрованные данные в виде строки
                    }
                }
            }

            return plaintext; // Возвращаем расшифрованную строку
        }
    }
}

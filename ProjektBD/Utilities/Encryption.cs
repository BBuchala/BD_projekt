using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace ProjektBD.Utilities
{
    /// <summary>
    /// Klasa służąca do szyfrowania haseł użytkowników.
    /// </summary>
    static class Encryption
    {
        /// <summary>
        /// Dopisuje do hasła podaną sól, po czym szyfruje algorytmem SHA256.
        /// </summary>
        /// <returns> Zaszyfrowane hasło </returns>
        public static string HashPassword(string password, string salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            string pswdWithSalt = password + salt;

            byte[] pswdByteArray = Encoding.UTF8.GetBytes(pswdWithSalt);        // Konwertuje posolone hasło na ciąg bajtów

            byte[] hashedPassword = algorithm.ComputeHash(pswdByteArray);       // Hashuje hasło, korzystając z algorytmu SHA256

            return Convert.ToBase64String(hashedPassword);
        }

        /// <summary>
        /// Generuje sól do hasła.
        /// </summary>
        public static string generateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            byte[] saltByteArray = new byte[12];
            rng.GetBytes(saltByteArray);

            return Convert.ToBase64String(saltByteArray);
        }
    }
}

//Encoding.UTF8.GetBytes(string);               <- obsługuje nawet kanji, przez co wynik również często skośny
//Encoding.UTF8.GetString(byte[]);

//Convert.FromBase64String(string);             <- nie obsługuje polskich znaków, za to wynik wygląda ludzko
//Convert.ToBase64String(byte[]);
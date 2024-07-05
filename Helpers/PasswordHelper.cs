using System;
using System.Linq;
using System.Security.Cryptography;

namespace TicTacToe.Helpers
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Хешує пароль за допомогою HMACSHA512 та генерує соль.
        /// </summary>
        /// <param name="password">Пароль, який необхідно хешувати.</param>
        /// <param name="salt">Вихідний параметр, що повертає згенеровану соль.</param>
        /// <returns>Хешований пароль у вигляді рядка Base64.</returns>
        public static string HashPassword(string password, out string salt)
        {
            using (var hmac = new HMACSHA512())
            {
                var saltBytes = hmac.Key;
                var hashedPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                salt = Convert.ToBase64String(saltBytes);
                return Convert.ToBase64String(hashedPassword);
            }
        }

        /// <summary>
        /// Перевіряє правильність пароля шляхом порівняння збереженого хешу та збереженої солі з введеним паролем.
        /// </summary>
        /// <param name="password">Пароль, який необхідно перевірити.</param>
        /// <param name="storedHash">Збережений хеш пароля у вигляді рядка Base64.</param>
        /// <param name="storedSalt">Збережена соль у вигляді рядка Base64.</param>
        /// <returns>Повертає true, якщо пароль правильний; в іншому випадку - false.</returns>
        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            using (var hmac = new HMACSHA512(saltBytes))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var storedHashBytes = Convert.FromBase64String(storedHash);

                return computedHash.SequenceEqual(storedHashBytes);
            }
        }
    }
}

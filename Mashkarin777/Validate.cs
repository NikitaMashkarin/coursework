using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashkarin777
{
    public class Validate
    {
        public Validate() { }

        public string ValidatePassword(string password)
        {
            StringBuilder passwordErrors = new StringBuilder();

            if (password.Length < 8)
            {
                passwordErrors.AppendLine("Пароль должен содержать хотя бы 8 символов");
            }
            if (!password.Any(char.IsLower))
            {
                passwordErrors.AppendLine("Пароль должен содержать хотя бы одну строчную букву");
            }
            if (!password.Any(char.IsUpper))
            {
                passwordErrors.AppendLine("Пароль должен содержать хотя бы одну прописную букву");
            }
            if (!password.Any(char.IsDigit))
            {
                passwordErrors.AppendLine("Пароль должен содержать хотя бы одну цифру");
            }
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                passwordErrors.AppendLine("Пароль должен содержать хотя бы один специальный символ");
            }

            return passwordErrors.ToString();
        }
    }
}

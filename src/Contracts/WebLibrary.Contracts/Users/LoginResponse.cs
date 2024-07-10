using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.Contracts.Users
{
    /// <summary>
    /// Ответ авторизации.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid id {  get; set; }

        /// <summary>
        /// Токен авторизации.
        /// </summary>
        public string Token { get; set; }
    }
}

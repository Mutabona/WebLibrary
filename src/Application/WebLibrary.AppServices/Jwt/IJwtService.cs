using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Contracts.Users;

namespace WebLibrary.AppServices.Jwt
{
    /// <summary>
    /// Сервис для работы с jwt токенами.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Создание jwt токена.
        /// </summary>
        /// <param name="userData">Запрос на создание <see cref="LoginRequest"/>.</param>
        /// <param name="id">Идентификатор польззователя.</param>
        /// <returns>Токен в виде строки.</returns>
        Task<string> GetToken(LoginRequest userData, Guid id);
    }
}

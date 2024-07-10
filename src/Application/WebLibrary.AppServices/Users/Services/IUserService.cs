using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Contracts.Users;

namespace WebLibrary.AppServices.Users.Services
{
    /// <summary>
    /// Сервис работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Возвращает всех пользователей.
        /// </summary>
        /// <returns>Список пользователей <see cref="UserDto"/></returns>
        Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Данные пользователя <see cref="UserDto"/>.</returns>
        ValueTask<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет пользователя.
        /// </summary>
        /// <param name="entity">Сущность пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task UpdateAsync(UserDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        
        /// <summary>
        /// Проверяет логин на уникальность.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task<bool> IsUniqueLogin(string login, CancellationToken cancellationToken);

        /// <summary>
        /// Авторизирует пользователя.
        /// </summary>
        /// <param name="request">Запрос на авторизацию <see cref="LoginRequest"/></param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ответ авторизации <see cref="LoginResponse"/></returns>
        Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Регистрирует пользователя в системе.
        /// </summary>
        /// <param name="request">Запрос на регистрицию.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserDto> RegisterAsync(RegistratinRequest request, CancellationToken cancellationToken);
    }
}

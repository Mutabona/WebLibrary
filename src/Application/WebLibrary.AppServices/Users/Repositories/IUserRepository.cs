using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Contracts.Users;

namespace WebLibrary.AppServices.Users.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Пользователи <see cref="IEnumerable<UserDto>"/>.</returns>
        Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Получение пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Данные пользователя <see cref="UserDto"/></returns>
        Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение пользователя по логину.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Данные пользователя <see cref="UserDto"/></returns>
        Task<UserDto> GetByLoginAsync(string login, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="entity">Пользователь <see cref="Domain.Users.Entity.User entity"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task AddAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken);

        /// <summary>
        /// Обноваление пользователя.
        /// </summary>
        /// <param name="entity">Пользователь.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task UpdateAsync(UserDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="request">Запрос на авторизацию <see cref="LoginRequest"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task<UserDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    }
}

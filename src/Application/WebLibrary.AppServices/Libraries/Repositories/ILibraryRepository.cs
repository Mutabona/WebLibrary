using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Domain.Library.Entity;
using WebLibrary.Contracts.Libraries;

namespace WebLibrary.AppServices.Libraries.Repositories
{
    /// <summary>
    /// Репозиторий для работы с библиотеками.
    /// </summary>
    public interface ILibraryRepository
    {
        /// <summary>
        /// Получение всех библиотек.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список всех библиотек <see cref="IEnumerable<LibraryDto>"/>.</returns>
        Task<IEnumerable<LibraryDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение библиотеки по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор библиотеки.</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Библиотека <see cref="LibraryDto"/>.</returns>
        Task<LibraryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление библиотеки.
        /// </summary>
        /// <param name="entity">Библиотека.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task AddAsync(Library entity, CancellationToken cancellationToken);

        /// <summary>
        /// Обновление библиотеки.
        /// </summary>
        /// <param name="entity">Библиотека.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task UpdateAsync(LibraryDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление библиотеки.
        /// </summary>
        /// <param name="id">Идентификатор библиотеки.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

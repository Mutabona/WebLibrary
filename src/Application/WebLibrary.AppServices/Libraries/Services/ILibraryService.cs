using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Contracts.Libraries;

namespace WebLibrary.AppServices.Libraries.Services
{
    /// <summary>
    /// Сервис для работы с библиотеками.
    /// </summary>
    public interface ILibraryService
    {
        /// <summary>
        /// Возвращает все библиотеки.
        /// </summary>
        /// <returns>Список библиотек <see cref="LibraryDto"/></returns>
        Task<IEnumerable<LibraryDto>> GetLibrariesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение библиотеки по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор библиотеки.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Данные библиотеки <see cref="LibraryDto"/>.</returns>
        ValueTask<LibraryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновление библиотеки.
        /// </summary>
        /// <param name="request">Данные библиотеки.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task UpdateAsync(LibraryDto request, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление библиотеки.
        /// </summary>
        /// <param name="id">Идентификатор библиотеки</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление библиотеки.
        /// </summary>
        /// <param name="entity">Сущность библиотеки.</param>
        /// <param name="cancellationToken">Токен омены.</param>
        /// <returns>Данные библиотеки <see cref="LibraryDto"/>.</returns>
        Task<LibraryDto> AddAsync(LibraryDto entity, CancellationToken cancellationToken);
    }
}

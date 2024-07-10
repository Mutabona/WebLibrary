using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Contracts.Files;
using WebLibrary.Domain.Files.Entity;

namespace WebLibrary.AppServices.Files.Repositories
{
    /// <summary>
    /// Репозиторий для работы с файлами.
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Получение информации о файле по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Информация о файле. <see cref="FileInfoDto"/></returns>
        Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление файле по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadAsync(Domain.Files.Entity.File file, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель скачиваемого файла.</returns>
        Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}

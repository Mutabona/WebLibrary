using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.AppServices.Files.Repositories;
using WebLibrary.Contracts.Files;

namespace WebLibrary.AppServices.Files.Services
{
    /// <summary>
    /// Сервис для работы с файлами.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Получение информации о файле по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Информация о файле.</returns>
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
        /// <param name="model">Модель файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadAsync(FileDto model, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель скачиваемого файла.</returns>
        Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Преобразования из файла в массив байт.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <returns>Массив байт.</returns>
        Task<byte[]> GetBytesAsync(IFormFile file);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebLibrary.AppServices.Files.Services;
using WebLibrary.AppServices.Libraries.Services;
using WebLibrary.Contracts.Files;
using WebLibrary.Contracts.Libraries;
using WebLibrary.Contracts.Users;
using WebLibrary.Domain.Users.Entity;

namespace WebLibrary.Api.Controllers
{
    /// <summary>
    /// Контреллер работы с библиотеками.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        private readonly IFileService _fileService;
        private readonly ILogger<LibraryController> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Создаёт экземпляр <see cref="LibraryController"/>.
        /// </summary>
        /// <param name="libraryService">Сервис для работы с библиотеками.</param>
        /// <param name="logger">Логгер.</param>
        /// <param name="fileService">Сервис для работы с файлами.</param>
        /// <param name="mapper">Маппер.</param>
        public LibraryController(ILibraryService libraryService, ILogger<LibraryController> logger, IFileService fileService, IMapper mapper)
        {
            _libraryService = libraryService;
            _logger = logger;
            _fileService = fileService;
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает все библиотеки.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Все библиотеки.</returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<LibraryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllLibraries(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на получение всех библиотек");
            var result = await _libraryService.GetLibrariesAsync(cancellationToken);
            _logger.LogInformation("Запрос на получение всех библиотек завершён успешно");

            return Ok(result);
        }

        /// <summary>
        /// Обновляет библиотеку.
        /// </summary>
        /// <param name="model">Модель библиотеки <see cref="LibraryDto"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("library/{id:Guid}")]
        public async Task<IActionResult> UpdateAsync(LibraryDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на обновление библиотеки с идентификатором: {id}", model.Id);
            var library = await _libraryService.GetByIdAsync(model.Id, cancellationToken);
            var libraryOwnerId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (library.OwnerId != libraryOwnerId) return new StatusCodeResult(403);
            await _libraryService.UpdateAsync(model, cancellationToken);
            _logger.LogInformation("Запрос на обновление библиотеки с идентификатором: {id} завершён успешно", model.Id);
            return Ok();
        }

        /// <summary>
        /// Удаляет библиотеку.
        /// </summary>
        /// <param name="id">Идентификатор библиотеки.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteLibrary(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на удаление библиотеки с идентификатором: {id}", id);
            var library = await _libraryService.GetByIdAsync(id, cancellationToken);
            var libraryOwnerId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (library.OwnerId != libraryOwnerId) return new StatusCodeResult(403);
            await _libraryService.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Запрос на удаление библиотеки с идентификатором: {id} завершён успешно", id);

            return Ok();
        }

        /// <summary>
        /// Возвращает библиотеку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Библиотека</returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на получение библиотеки с идентификатором: {id}", id);
            var result = await _libraryService.GetByIdAsync(id, cancellationToken);
            _logger.LogInformation("Запрос на получение библиотеки с идентификатором: {id} завершён успешно", id);

            return Ok(result);
        }

        /// <summary>
        /// Добавление библиотеки.
        /// </summary>
        /// <param name="request">Запрос на создание библиотеки <see cref="CreateLibraryRequest"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync(CreateLibraryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на получение библиотеки: {name}", request.Name);
            var name = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name);
            var id = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            var library = new LibraryDto
            {
                Name = request.Name,
                IsPublic = request.IsPublic,
                OwnerName = name.Value,
                OwnerId = new Guid(id.Value)
            };
            var response = await _libraryService.AddAsync(library, cancellationToken);
            _logger.LogInformation("Запрос на получение библиотеки: {name} завершён успешно", request.Name);
            return Ok(response); 
        }

        /// <summary>
        /// Загрузка файла.
        /// </summary>
        /// <param name="file">Файл <see cref="IFormFile"/>.</param>
        /// <param name="libraryId">Идентификатор библиотеки.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("{libraryId}")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Authorize]
        public async Task<IActionResult> UploadFile([FromRoute] Guid libraryId, IFormFile file, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на загрузку файла в библиотеку: {id}", libraryId);
            var library = await _libraryService.GetByIdAsync(libraryId, cancellationToken);
            var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (library.OwnerId != userId) return new StatusCodeResult(403);

            var fileDto = new FileDto
            {
                Name = file.FileName,
                Content = await _fileService.GetBytesAsync(file),
                LibraryId = libraryId,
                ContentType = file.ContentType
            };
            var result = await _fileService.UploadAsync(fileDto, cancellationToken);
            library.FilesAmount++;
            await _libraryService.UpdateAsync(library, cancellationToken);
            _logger.LogInformation("Запрос на загрузку файла в библиотеку: {id} завершён успешно", libraryId);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Загрузка файла.
        /// </summary>
        /// <param name="libraryId">Идентификатор библиотеки.</param>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [HttpGet("{libraryId}/file/{fileId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DownloadFile([FromRoute] Guid libraryId, [FromRoute] Guid fileId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на загрузку файла из библиотеки: {id}", libraryId);
            var library = await _libraryService.GetByIdAsync(libraryId, cancellationToken);
            var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (!library.IsPublic && library.OwnerId != userId) return new StatusCodeResult(403);

            var result = await _fileService.DownloadAsync(fileId, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            Response.ContentLength = result.Content.Length;
            _logger.LogInformation("Запрос на загрузку файла из библиотеки: {id} завершён успешно", libraryId);
            return File(result.Content, result.ContentType);
        }

        /// <summary>
        /// Удаление файла.
        /// </summary>
        /// <param name="libraryId">Идентификатор библиотеки.</param>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{libraryId}/file/{fileId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteFile([FromRoute] Guid libraryId, [FromRoute] Guid fileId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на удаление файла из библиотеки: {id}", libraryId);
            var library = await _libraryService.GetByIdAsync(libraryId, cancellationToken);
            var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (library.OwnerId != userId) return new StatusCodeResult(403);

            await _fileService.DeleteByIdAsync(fileId, cancellationToken);
            library.FilesAmount--;
            await _libraryService.UpdateAsync(library, cancellationToken);
            _logger.LogInformation("Запрос на удаление файла из библиотеки: {id} завершён успешно", libraryId);
            return NoContent();
        }

        /// <summary>
        /// Получение информации о файле по его идентификатору.
        /// </summary>
        /// <param name="libraryId">Идентификатор библиотеки.</param>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о файле.</returns>
        [HttpGet("{libraryId}/file/{fileId}/info")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetFileInfoById([FromRoute] Guid libraryId, [FromRoute] Guid fileId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на получение информации о файле: {id}", fileId);
            var library = await _libraryService.GetByIdAsync(libraryId, cancellationToken);
            if (!library.IsPublic) return new StatusCodeResult(403);

            var result = await _fileService.GetInfoByIdAsync(fileId, cancellationToken);
            _logger.LogInformation("Запрос на получение информации о файле: {id} завершён успешно", fileId);
            return result == null ? NotFound() : Ok(result);
        }
    }
}

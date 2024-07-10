using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebLibrary.AppServices.Users.Services;
using WebLibrary.Contracts.Users;
using WebLibrary.Domain.Users.Entity;

namespace WebLibrary.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserController"/>
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями</param>
        /// <param name="logger">Логгер.</param>
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает список пользователей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список пользователей</returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на получение всех пользователей");
            var result = await _userService.GetUsersAsync(cancellationToken);
            _logger.LogInformation("Запрос на получение всех пользователей завершён успешно");

            return Ok(result);
        }

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="request">Запрос на создание пользователя<see cref="RegistratinRequest"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegistratinRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на регистрацию пользователя: {name}", request.Login);
            var result = await _userService.RegisterAsync(request, cancellationToken);

            if (result == null) 
            {
                _logger.LogInformation("Пользователь с таким логином уже существует {login}", request.Login);
                return BadRequest(new { message = "Username already taken" });
            }

            _logger.LogInformation("Запрос на регистрацию пользователя: {name} завершён успешно", request.Login);
            return Ok(result);
        }

        /// <summary>
        /// Впускает пользователя.
        /// </summary>
        /// <param name="request">Запрос на вход<see cref="LoginRequest"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ответ входа <see cref="LoginResponse"/>.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(IEnumerable<LoginResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на вход в пользователя: {login}", request.Login);
            LoginResponse response = await _userService.LoginAsync(request, cancellationToken);

            if (response == null)
            {
                _logger.LogInformation("Неврная попытка входа в аккаунт: {login}", request.Login);
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            _logger.LogInformation("Запрос на вход в пользователя: {login} завершён успешно", request.Login);
            return Ok(response);
        }

        /// <summary>
        /// Обновленяет пользователя.
        /// </summary>
        /// <param name="user">Пользователь <see cref="UserDto"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateUser(UserDto user, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на обновление пользователя: {login}", user.Login);
            var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userId != user.Id) return new StatusCodeResult(403);

            await _userService.UpdateAsync(user, cancellationToken);
            _logger.LogInformation("Запрос на обновление пользователя: {login} завершён успешно", user.Login);
            return Ok();
        }

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на удаление пользователя с идентификатором: {id}", id);
            var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userId != id) return new StatusCodeResult(403);

            await _userService.DeleteAsync(id, cancellationToken);

            _logger.LogInformation("Запрос на удаление пользователя с идентификатором: {id} завершён успешно", id);
            return Ok();
        }

        /// <summary>
        /// Возвращает пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Пользователь <see cref="UserDto"/></returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на получение пользователя с идентификатором: {id}", id);
            var result = await _userService.GetByIdAsync(id, cancellationToken);
            _logger.LogInformation("Запрос на получение пользователя с идентификатором: {id} завершён успешно", id);

            return Ok(result);
        }
    }
}

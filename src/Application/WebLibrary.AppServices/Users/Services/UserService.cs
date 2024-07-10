using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.AppServices.Jwt;
using WebLibrary.AppServices.Users.Repositories;
using WebLibrary.Contracts.Users;
using WebLibrary.Domain.Users.Entity;

namespace WebLibrary.AppServices.Users.Services
{
    ///<inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _jwtService = jwtService;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public async ValueTask<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Отправка запроса на получение всех пользователей");
            return _userRepository.GetAll(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> IsUniqueLogin(string login, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByLoginAsync(login, cancellationToken);
            if (user == null) return true;
            return false;
        }

        /// <inheritdoc/>
        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.LoginAsync(request, cancellationToken);


            if (user == null)
            {
                return null;
            }

            var response = new LoginResponse()
            {
                id = user.Id,
                Token = await _jwtService.GetToken(request, user.Id)              
            };
            return response;
        }

        /// <inheritdoc/>
        public async Task<UserDto> RegisterAsync(RegistratinRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Отправка запроса на регистрацию пользователя");
            if (!IsUniqueLogin(request.Login, cancellationToken).Result)
            {
                _logger.LogInformation("Пользователь с таким логином уже существует: {login}", request.Login);
                return null;
            }
            var user = _mapper.Map<User>(request);

            await _userRepository.AddAsync(user, cancellationToken);

            _logger.LogInformation("Успешная регистрация пользователя: {login}", request.Login);
            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(UserDto entity, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}

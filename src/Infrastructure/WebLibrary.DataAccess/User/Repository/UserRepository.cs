using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.AppServices.Users.Repositories;
using WebLibrary.Contracts.Users;
using WebLibrary.Domain.Users.Entity;
using WebLibrary.Infrastructure.Repository;


namespace WebLibrary.DataAccess.User.Repository
{
    ///<inheritdoc cref="IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Users.Entity.User> _repository;

        /// <summary>
        /// Создаёт экземпляр <see cref="UserRepository"/>
        /// </summary>
        /// <param name="repository">Репозиторий.</param>
        /// <param name="mapper">Маппер.</param>
        public UserRepository(IRepository<Domain.Users.Entity.User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            
            var users = await _repository.GetAll().ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return users;
        }

        ///<inheritdoc/>
        public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }

        ///<inheritdoc/>
        public async Task AddAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(entity, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(UserDto entity, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Users.Entity.User>(entity);
            await _repository.UpdateAsync(user, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<UserDto> GetByLoginAsync(string login, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAll().Where(s => s.Login == login).ProjectTo<UserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
            return user;
        }

        ///<inheritdoc/>
        public async Task<UserDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAll().Where(s => s.Login == request.Login && s.Password == request.Password)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}

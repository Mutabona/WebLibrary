using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.AppServices.Libraries.Repositories;
using WebLibrary.Contracts.Libraries;
using WebLibrary.Contracts.Users;
using WebLibrary.Infrastructure.Repository;

namespace WebLibrary.DataAccess.Library.Repository
{
    ///<inheritdoc cref="LibraryRepository"/>
    public class LibraryRepository : ILibraryRepository
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Library.Entity.Library> _repository;

        /// <summary>
        /// Создаёт экземпляр <see cref="LibraryRepository"/>
        /// </summary>
        /// <param name="mapper">Маппер.</param>
        /// <param name="repository">Рупозиторий.</param>
        public LibraryRepository(IMapper mapper, IRepository<Domain.Library.Entity.Library> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        ///<inheritdoc/>
        public async Task AddAsync(Domain.Library.Entity.Library entity, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(entity, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<LibraryDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var libraries = await _repository.GetAll().ProjectTo<LibraryDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return libraries;
        }

        ///<inheritdoc/>
        public async Task<LibraryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var library = await _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<LibraryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return library;
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(LibraryDto entity, CancellationToken cancellationToken)
        {
            var library = _mapper.Map<Domain.Library.Entity.Library>(entity);
            await _repository.UpdateAsync(library, cancellationToken);
        }
    }
}

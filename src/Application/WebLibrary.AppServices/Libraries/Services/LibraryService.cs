using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.AppServices.Libraries.Repositories;
using WebLibrary.AppServices.Users.Repositories;
using WebLibrary.Contracts.Libraries;
using WebLibrary.Domain.Library.Entity;

namespace WebLibrary.AppServices.Libraries.Services
{
    ///<inheritdoc cref="LibraryService"/>
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LibraryService> _logger;

        /// <summary>
        /// Создание экземпляра <see cref="LibraryService"/>.
        /// </summary>
        /// <param name="libraryRepository">Репозиторий</param>
        /// <param name="mapper">Маппер</param>
        /// <param name="logger">Логгер</param>
        public LibraryService(ILibraryRepository libraryRepository, IMapper mapper, ILogger<LibraryService> logger)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        ///<inheritdoc/>
        public async Task<LibraryDto> AddAsync(LibraryDto request, CancellationToken cancellationToken)
        {
            var library = _mapper.Map<Library>(request);
            await _libraryRepository.AddAsync(library, cancellationToken);
            return _mapper.Map<LibraryDto>(library);
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _libraryRepository.DeleteAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public async ValueTask<LibraryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _libraryRepository.GetByIdAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<LibraryDto>> GetLibrariesAsync(CancellationToken cancellationToken)
        {
            return await _libraryRepository.GetAllAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(LibraryDto entity, CancellationToken cancellationToken)
        {
            await _libraryRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}

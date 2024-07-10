using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.AppServices.Files.Repositories;
using WebLibrary.Contracts.Files;
using WebLibrary.Infrastructure.Repository;

namespace WebLibrary.DataAccess.Files.Repository
{
    /// <inheritdoc cref="IFileRepository"/>
    public class FileRepository : IFileRepository
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Files.Entity.File> _repository;

        public FileRepository(IMapper mapper, IRepository<Domain.Files.Entity.File> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        ///<inheritdoc/>
        public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<FileDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<FileInfoDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(Domain.Files.Entity.File file, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(file, cancellationToken);
            return file.Id;
        }
    }
}

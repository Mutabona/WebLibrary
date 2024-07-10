using AutoMapper;
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
    /// <inheritdoc cref="IFileService"/>
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует экземпляр <see cref="FileService"/>
        /// </summary>
        /// <param name="fileRepository">Репозиторий.</param>
        /// <param name="mapper">Маппер.</param>
        public FileService(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _fileRepository.DeleteByIdAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _fileRepository.DownloadAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _fileRepository.GetInfoByIdAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public Task<Guid> UploadAsync(FileDto model, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<Domain.Files.Entity.File>(model);
            return _fileRepository.UploadAsync(file, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<byte[]> GetBytesAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}

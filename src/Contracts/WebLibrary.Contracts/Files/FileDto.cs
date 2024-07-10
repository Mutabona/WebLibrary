using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.Contracts.Files
{
    /// <summary>
    /// Модель файла.
    /// </summary>
    public class FileDto
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Контент файла.
        /// </summary>
        public byte[] Content { get; set; }
        /// <summary>
        /// Тип контента файла.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Идентификатор библиотеки файла.
        /// </summary>
        public Guid LibraryId { get; set; }
    }
}

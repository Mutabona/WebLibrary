using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Domain.Base;

namespace WebLibrary.Domain.Files.Entity
{
    /// <summary>
    /// Сущность файла.
    /// </summary>
    public class File : BaseEntity
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
        /// Размер файла.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Идентификатор библиотеки файла.
        /// </summary>
        public Guid LibraryId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Domain.Base;

namespace WebLibrary.Domain.Library.Entity
{
    /// <summary>
    /// Сущность библиотеки.
    /// </summary>
    public class Library : BaseEntity
    {
        /// <summary>
        /// Идентификатор владельца.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Название библиотеки.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество файлов в библиотеке.
        /// </summary>
        public int FilesAmount { get; set; }

        /// <summary>
        /// Имя владельца.
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// Флаг публичности библиотеки.
        /// </summary>
        public bool IsPublic { get; set; }
    }
}

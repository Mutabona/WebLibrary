using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.Contracts.Libraries
{
    /// <summary>
    /// Запрос на создание библиотеки.
    /// </summary>
    public class CreateLibraryRequest
    {
        /// <summary>
        /// Название библиотеки.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Флаг публичности библиотеки.
        /// </summary>
        public bool IsPublic { get; set; }
    }
}

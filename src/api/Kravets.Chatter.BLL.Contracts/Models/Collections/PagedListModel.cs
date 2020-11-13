using System.Collections.Generic;

namespace Kravets.Chatter.BLL.Contracts.Models.Collections
{
    /// <summary>
    /// Represents paged list.
    /// </summary>
    /// <typeparam name="T">T.</typeparam>
    public class PagedListModel<T>
    {
        /// <summary>
        /// Result collection.
        /// </summary>
        public IEnumerable<T> Result { get; private set; }
        /// <summary>
        /// Current page index.
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// Flag that shows if next page exists.
        /// </summary>
        public bool NextPageExists { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="result">Result collection.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="nextPageExists">Flag that shows if next page exists.</param>
        public PagedListModel(
            IEnumerable<T> result,
            int pageIndex,
            int pageSize,
            bool nextPageExists) =>
        (Result, PageIndex, PageSize, NextPageExists) = (result, pageIndex, pageSize, nextPageExists);
    }
}

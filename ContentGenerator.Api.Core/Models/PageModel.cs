using System.Diagnostics.CodeAnalysis;

namespace ContentGenerator.Api.Core.Models
{
    [ExcludeFromCodeCoverage]
    public class PageModel<T> where T : class
    {
        private long? _numberPage;
        private long? _tableCount;
        private long _itemsPerPage;
        private string _sortOrder;
        private string _sortColumn;

        public long TotalRecords { get; set; }
        public IEnumerable<T> Items { get; set; } = [];
        public long ItemsPerPage
        {
            get
            {
                if (_itemsPerPage != 0L)
                {
                    return _itemsPerPage;
                }
                return 30L;
            }
            private set
            {
                _itemsPerPage = value;
            }
        }
        public long? PageNumber
        {
            get
            {
                if (!_numberPage.HasValue || _numberPage.Value == 0L)
                {
                    return 1L;
                }

                return _numberPage.Value;
            }
            private set
            {
                _numberPage = value;
            }
        }
        public long TotalCount
        {
            get
            {
                if (!_tableCount.HasValue)
                {
                    _tableCount = 0L;
                }

                return _tableCount.Value;
            }
            set
            {
                _tableCount = value;
            }
        }
        public long TotalPages
        {
            get
            {
                if (Items != null && Items.Any())
                {
                    long num = Items.Count();
                    if (num < TotalCount)
                    {
                        if (TotalCount % num == 0L && num == ItemsPerPage)
                        {
                            return TotalCount / ItemsPerPage;
                        }

                        return TotalCount / ItemsPerPage + 1;
                    }

                    return 1L;
                }

                return 0L;
            }
        }
        public string SortOrder
        {
            get
            {
                return _sortOrder;
            }
            private set
            {
                _sortOrder = value;
            }
        }
        public string SortColumn
        {
            get
            {
                return _sortColumn;
            }
            private set
            {
                _sortColumn = value;
            }
        }
        public long OffSetNumber => PageNumber.Value * ItemsPerPage;

        public PageModel(int itemsPerPage, int? numberPage = 1, long? totalItems = null, string sortOrder = "ASC", string sortColumn = "FULLNAME")
        {
            _numberPage = numberPage;
            ItemsPerPage = itemsPerPage;
            _sortColumn = sortColumn;
            _sortOrder = sortOrder;
            _tableCount = totalItems;

        }
    }
}

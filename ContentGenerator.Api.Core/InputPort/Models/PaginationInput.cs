using System.Diagnostics.CodeAnalysis;

namespace ContentGenerator.Api.Core.InputPort.Models
{
    [ExcludeFromCodeCoverage]
    public class PaginationInput
    {
        public int? PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public string SortOrder { get; set; }
        public string SortColumn { get; set; }

        public PaginationInput(int? pageNumber, int itemsPerPage, string sortOrder, string sortColumn)
        {
            PageNumber = pageNumber;
            ItemsPerPage = itemsPerPage;
            SortOrder = sortOrder;
            SortColumn = sortColumn;
        }
    }
}

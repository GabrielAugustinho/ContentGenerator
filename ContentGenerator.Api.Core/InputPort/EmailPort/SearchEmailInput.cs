using ContentGenerator.Api.Core.InputPort.Models;

namespace ContentGenerator.Api.Core.InputPort.EmailPort
{
    public class SearchEmailInput
    {
        public bool? Active { get; set; }
        public PaginationInput Pagination { get; set; }
    }
}

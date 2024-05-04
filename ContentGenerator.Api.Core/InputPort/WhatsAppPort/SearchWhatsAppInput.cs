using ContentGenerator.Api.Core.InputPort.Models;

namespace ContentGenerator.Api.Core.InputPort.WhatsAppPort
{
    public class SearchWhatsAppInput
    {
        public bool? Active { get; set; }

        public PaginationInput Pagination { get; set; }
    }
}

using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.Models;

namespace ContentGenerator.Api.Core.UseCases.PublicationCase.Interfaces
{
    public interface IAddPublication
    {
        public Task<bool> Execute(PublicationInput input, TwillioKeysModel keys);
    }
}

using ContentGenerator.Api.Core.InputPort.PublicationPort;

namespace ContentGenerator.Api.Core.UseCases.PublicationCase
{
    public interface IAddPublication
    {
        public Task<bool> Execute(PublicationInput input);
    }
}

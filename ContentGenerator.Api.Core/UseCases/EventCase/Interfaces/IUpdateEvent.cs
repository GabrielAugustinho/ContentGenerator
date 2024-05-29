using ContentGenerator.Api.Core.InputPort.EventPort;

namespace ContentGenerator.Api.Core.UseCases.EventCase.Interfaces
{
    public interface IUpdateEvent
    {
        Task<bool> Execute(UpdateEventInput input);
    }
}

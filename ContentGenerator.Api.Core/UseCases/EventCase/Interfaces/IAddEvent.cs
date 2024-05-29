using ContentGenerator.Api.Core.InputPort.EventPort;

namespace ContentGenerator.Api.Core.UseCases.EventCase.Interfaces
{
    public interface IAddEvent
    {
        Task<bool> Execute(AddEventInput input);
    }
}

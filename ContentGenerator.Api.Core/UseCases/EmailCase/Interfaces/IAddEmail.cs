using ContentGenerator.Api.Core.InputPort.EmailPort;

namespace ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces
{
    public interface IAddEmail
    {
        Task<bool> Execute(AddEmailInput input);
    }
}

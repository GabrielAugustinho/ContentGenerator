using ContentGenerator.Api.Core.InputPort.EmailPort;

namespace ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces
{
    public interface IUpdateEmail
    {
        Task<bool> Execute(UpdateEmailInput input);
    }
}

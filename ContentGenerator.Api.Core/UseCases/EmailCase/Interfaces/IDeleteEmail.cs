namespace ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces
{
    public interface IDeleteEmail
    {
        Task<bool> Execute(int id);
    }
}

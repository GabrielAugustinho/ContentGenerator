namespace ContentGenerator.Api.Core.UseCases.EventCase.Interfaces
{
    public interface IDeleteEvent
    {
        Task<bool> Execute(int id);
    }
}

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces
{
    public interface IDeleteWhatsApp
    {
        Task<bool> Execute(int id);
    }
}

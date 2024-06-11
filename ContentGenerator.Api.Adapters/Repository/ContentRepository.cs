using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.Services.Interfaces.ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Database.Context;

public class ContentRepository : IContentRepository
{
    private readonly DataContext _context;
    private readonly ILoggerService<ContentRepository> _logger;

    public ContentRepository(DataContext context, ILoggerService<ContentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> AddContent(ContentInput input)
    {
        try
        {
            var assunto = new Assunto
            {
                TipoValidacaoId = input.TipoValidacaoId,
                HumorId = input.HumorId,
                DestinosId = input.DestinosId,
                TipoAssuntoId = input.TipoAssuntoId,
                DataCriacao = input.DataCriacao,
                ObjEveAssunto = input.ObjEveAssunto,
                DataGeracao = input.DataGeracao,
                PostOriginal = input.PostOriginal,
                ImagemPost = input.ImagemPost,
                DataValida = null,
                PostValidado = null,
                DataPublicacao = null,
                IncluirImg = true, // or set based on input
                Ativo = true
            };

            _context.Assunto.Add(assunto);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add content to the database.");
            return false;
        }
    }
}

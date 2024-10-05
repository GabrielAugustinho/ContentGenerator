using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ContentRepository : IContentRepository
{
    private readonly DataContext _context;
    private readonly ILogger<ContentRepository> _logger;

    public ContentRepository(DataContext context, ILogger<ContentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int?> AddContent(ContentInput input)
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
                IncluirImg = false,
                Ativo = true
            };

            _context.Assunto.Add(assunto);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Content successfully added to the database with ID {assunto.AssuntoId}.");
            return assunto.AssuntoId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add content to the database.");
            return null;
        }
    }

    public async Task<Assunto?> GetContentById(int id)
    {
        try
        {
            _logger.LogInformation($"Retrieving content with ID {id} from the database.");

            var assunto = await _context.Assunto
                .Include(a => a.Destinos)
                .Include(a => a.Humor)
                .Include(a => a.TipoValidacao)
                .Include(a => a.TipoAssunto)
                .FirstOrDefaultAsync(a => a.AssuntoId == id);

            if (assunto == null)
            {
                _logger.LogWarning($"Content with ID {id} not found in the database.");
                return null;
            }

            _logger.LogInformation($"Content with ID {id} retrieved from the database.");
            return assunto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to retrieve content with ID {id} from the database.");
            return null;
        }
    }


    public async Task<IEnumerable<Assunto>> GetContentOfMonth()
    {
        try
        {
            _logger.LogInformation("Fetching contents for the current month.");
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var assuntos = await _context.Assunto
                .Where(a => a.Ativo && a.DataCriacao.Month == currentMonth && a.DataCriacao.Year == currentYear)
                .Include(a => a.Destinos)
                .Include(a => a.Humor)
                .Include(a => a.TipoValidacao)
                .Include(a => a.TipoAssunto)
                .ToListAsync();

            _logger.LogInformation("Contents for the current month fetched successfully.");
            return assuntos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch contents for the current month.");
            return [];
        }
    }

    public async Task<bool> UpdateContent(UpdateContentInput input)
    {
        try
        {
            _logger.LogInformation("Updating content with ID {Id} in the database.", input.ContentId);
            var existingAssunto = await _context.Assunto.FirstOrDefaultAsync(h => h.AssuntoId == input.ContentId);

            if (existingAssunto == null)
            {
                _logger.LogWarning("Content with ID {Id} not found in the database.", input.ContentId);
                return false;
            }

            existingAssunto.TipoValidacaoId = input.ValidationId;
            existingAssunto.DataValida = DateTime.UtcNow;
            existingAssunto.PostValidado = input.PostValidated;
            existingAssunto.ImagemPost = input.PostImage;
            existingAssunto.IncluirImg = input.UserImage;
            existingAssunto.Ativo = input.Active;

            _context.Assunto.Update(existingAssunto);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Content with ID {Id} updated successfully in the database.", input.ContentId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update content with ID {Id} in the database.", input.ContentId);
            return false;
        }
    }

    public async Task<bool> PublishContent(Assunto input)
    {
        try
        {
            _context.Assunto.Update(input);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Content with ID {Id} updated successfully in the database.", input.AssuntoId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update content with ID {Id} in the database.", input.AssuntoId);
            return false;
        }
    }
}

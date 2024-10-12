using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.Models;
using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.OutputPort.PostPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ContentRepository> _logger;

        public PostRepository(DataContext context, ILogger<ContentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int?> AddPost(PostInput input)
        {
            try
            {
                var post = new Publicacao
                {
                    AssuntoId = input.AssuntoId
                };

                _context.Publicacao.Add(post);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Post successfully added to the database with ID {post.PublicacaoId}.");
                return post.PublicacaoId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add post to the database.");
                return null;
            }
        }

        public async Task<IEnumerable<SearchPostOutput>> GetPublicationsPaged(PaginationInput input)
        {
            _logger.LogInformation("Iniciando a busca paginada de publicações.");

            try
            {
                int startIndex = (input.PageNumber - 1) * input.ItemsPerPage ?? 0;
                IQueryable<Publicacao> query = _context.Publicacao.Include(p => p.Assunto)
                                                                  .ThenInclude(a => a.TipoValidacao)
                                                                  .Include(p => p.Assunto.Humor)
                                                                  .Include(p => p.Assunto.Destinos)
                                                                  .Include(p => p.Assunto.TipoAssunto);

                if (input.Active is not null)
                {
                    query = query.Where(p => p.Assunto.Ativo == input.Active);
                }

                int totalCount = await query.CountAsync();

                if (!string.IsNullOrEmpty(input.SortColumn))
                {
                    var parameter = Expression.Parameter(typeof(Publicacao), "p");
                    var property = Expression.Property(parameter, input.SortColumn);
                    var lambda = Expression.Lambda(property, parameter);

                    query = input.SortOrder.ToLower() switch
                    {
                        "asc" => Queryable.OrderBy(query, (dynamic)lambda),
                        "desc" => Queryable.OrderByDescending(query, (dynamic)lambda),
                        _ => query
                    };
                }

                query = query.Skip(startIndex).Take(input.ItemsPerPage);

                var publicacoes = await query.ToListAsync();

                _logger.LogInformation("Busca paginada de publicações concluída com sucesso.");

                return publicacoes.Select(p => new SearchPostOutput(
                                          p.PublicacaoId,
                                          p.AssuntoId,
                                          p.Assunto.TipoValidacao?.Tipo,
                                          p.Assunto.Humor?.Descricao,
                                          p.Assunto.Destinos?.Descricao,
                                          p.Assunto.TipoAssunto?.Assunto,
                                          p.Assunto.DataCriacao,
                                          p.Assunto.ObjEveAssunto,
                                          p.Assunto.DataGeracao,
                                          p.Assunto.PostOriginal,
                                          p.Assunto.DataValida,
                                          p.Assunto.PostValidado,
                                          p.Assunto.DataPublicacao,
                                          p.Assunto.ImagemPost,
                                          p.Assunto.IncluirImg,
                                          p.Assunto.Ativo,
                                          totalCount)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar a busca paginada de publicações.");
                return Enumerable.Empty<SearchPostOutput>();
            }
        }
    }
}

using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.OutputPort.EmailPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<EmailRepository> _logger;

        public EmailRepository(DataContext dataContext, ILogger<EmailRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<bool> AddEmail(AddEmailInput input)
        {
            _logger.LogInformation("Adicionando novo email.");

            try
            {
                var email = new Email() { NomeCliente = input.Nome, EmailCliente = input.Email };

                _dataContext.Email.Add(email);
                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    _logger.LogInformation("Email adicionado com sucesso.");
                }
                else
                {
                    _logger.LogWarning("Erro ao adicionar email.");
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar email.");
                return false;
            }
        }

        public async Task<bool> DeleteEmail(int id)
        {
            _logger.LogInformation("Deletando email com ID: {Id}", id);

            try
            {
                var dbEmail = await _dataContext.Email.FindAsync(id);

                if (dbEmail is null)
                {
                    _logger.LogWarning("Email com ID {Id} não encontrado.", id);
                    return false;
                }

                dbEmail.Delete();
                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    _logger.LogInformation("Email com ID {Id} deletado com sucesso.", id);
                }
                else
                {
                    _logger.LogWarning("Erro ao deletar email com ID {Id}.", id);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar email com ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> UpdateEmail(UpdateEmailInput input)
        {
            _logger.LogInformation("Atualizando email com ID: {Id}", input.Id);

            try
            {
                var dbEmail = await _dataContext.Email.FindAsync(input.Id);

                if (dbEmail is null)
                {
                    _logger.LogWarning("Email com ID {Id} não encontrado.", input.Id);
                    return false;
                }

                dbEmail.Update(input.Nome, input.Email);
                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    _logger.LogInformation("Email com ID {Id} atualizado com sucesso.", input.Id);
                }
                else
                {
                    _logger.LogWarning("Erro ao atualizar email com ID {Id}.", input.Id);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar email com ID {Id}.", input.Id);
                return false;
            }
        }

        public async Task<SearchEmailOutput?> GetEmailById(int id)
        {
            _logger.LogInformation("Buscando email por ID: {Id}", id);

            try
            {
                var email = await _dataContext.Email.FindAsync(id);

                if (email is null)
                {
                    _logger.LogWarning("Email com ID {Id} não encontrado.", id);
                    return default;
                }

                _logger.LogInformation("Email com ID {Id} encontrado com sucesso.", id);
                return EmailToOutput(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar email com ID {Id}.", id);
                return default;
            }
        }

        public async Task<IEnumerable<SearchEmailOutput>> GetEmailPaged(SearchEmailInput input)
        {
            _logger.LogInformation("Buscando emails paginados.");

            try
            {
                int startIndex = (input.Pagination.PageNumber - 1) * input.Pagination.ItemsPerPage ?? 0;

                IQueryable<Email> query = _dataContext.Email;

                if (input.Active is not null)
                {
                    query = query.Where(x => x.Ativo == input.Active);
                }

                int totalCount = await query.CountAsync();

                if (!string.IsNullOrEmpty(input.Pagination.SortColumn))
                {
                    var parameter = Expression.Parameter(typeof(Email), "x");
                    var property = Expression.Property(parameter, input.Pagination.SortColumn);
                    var lambda = Expression.Lambda(property, parameter);

                    query = input.Pagination.SortOrder.ToLower() switch
                    {
                        "asc" => Queryable.OrderBy(query, (dynamic)lambda),
                        "desc" => Queryable.OrderByDescending(query, (dynamic)lambda),
                        _ => query
                    };
                }

                query = query.Skip(startIndex).Take(input.Pagination.ItemsPerPage);

                var emails = await query.ToListAsync();
                _logger.LogInformation("Busca paginada de emails concluída com sucesso.");
                return ListEmailToListOutput(emails, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar emails paginados.");
                return Enumerable.Empty<SearchEmailOutput>();
            }
        }

        private static List<SearchEmailOutput> ListEmailToListOutput(List<Email> emails, int totalCount)
        {
            return emails.Select(item => new SearchEmailOutput(item.EmailId, item.NomeCliente, item.EmailCliente, item.Ativo, totalCount)).ToList();
        }

        private static SearchEmailOutput EmailToOutput(Email email)
        {
            return new SearchEmailOutput(email.EmailId, email.NomeCliente, email.EmailCliente, email.Ativo, 1);
        }
    }
}

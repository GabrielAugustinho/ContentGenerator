using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.OutputPort.EmailPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DataContext _dataContext;

        public EmailRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddEmail(AddEmailInput input)
        {
            var email = new Email() { NomeCliente = input.Nome, EmailCliente = input.Email };

            _dataContext.Email.Add(email);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteEmail(int id)
        {
            Email? dbEmail = await _dataContext.Email.FindAsync(id);

            if (dbEmail is null)
                return false;

            dbEmail.Delete();

            var result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateEmail(UpdateEmailInput input)
        {
            Email? dbEmail = await _dataContext.Email.FindAsync(input.Id);

            if (dbEmail is null)
                return false;

            dbEmail.Update(input.Nome, input.Email);

            var result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<SearchEmailOutput?> GetEmailById(int id)
        {
            Email? email = await _dataContext.Email.FindAsync(id);

            if (email is null)
                return default;

            return EmailToOutput(email);
        }

        public async Task<IEnumerable<SearchEmailOutput>> GetEmailPaged(SearchEmailInput input)
        {
            int startIndex = (input.Pagination.PageNumber - 1) * input.Pagination.ItemsPerPage ?? 0;

            IQueryable<Email> query = _dataContext.Email;

            if (input.Active is not null)
                query = query.Where(x => x.Ativo == input.Active);

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

            List<Email> emails = await query.ToListAsync();
            return ListEmailToListOutput(emails, totalCount);
        }

        private static List<SearchEmailOutput> ListEmailToListOutput(List<Email> email, int totalCount)
        {
            var output = new List<SearchEmailOutput>();

            foreach (var item in email)
                output.Add(new SearchEmailOutput(item.EmailId, item.NomeCliente, item.EmailCliente, item.Ativo, totalCount));

            return output;
        }

        private static SearchEmailOutput EmailToOutput(Email email)
        {
            var output = new SearchEmailOutput(email.EmailId, email.NomeCliente, email.EmailCliente, email.Ativo, 1);
            return output;
        }
    }
}

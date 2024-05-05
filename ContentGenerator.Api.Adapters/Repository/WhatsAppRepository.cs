using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContentGenerator.Api.Adapters.Repository.WhatsAppRepo
{
    public class WhatsAppRepository : IWhatsAppRepository
    {
        private readonly DataContext _dataContext;

        public WhatsAppRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<SearchWhatsAppOutput?> GetWhatsAppById(int id)
        {
            WhatsApp? whatsApp = await _dataContext.WhatsApp.FindAsync(id);

            if (whatsApp is null)
                return default;

            return WhatsAppToOutput(whatsApp);
        }

        public async Task<IEnumerable<SearchWhatsAppOutput>> GetWhatsAppPaged(SearchWhatsAppInput input)
        {
            int startIndex = (input.Pagination.PageNumber - 1) * input.Pagination.ItemsPerPage ?? 0;

            IQueryable<WhatsApp> query = _dataContext.WhatsApp;

            if(input.Active is not null)
                query = query.Where(x => x.Ativo == input.Active);

            int totalCount = await query.CountAsync();

            if (!string.IsNullOrEmpty(input.Pagination.SortColumn))
            {
                var parameter = Expression.Parameter(typeof(WhatsApp), "x");
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

            List<WhatsApp> whatsApps = await query.ToListAsync();
            return ListWhatsAppToListOutput(whatsApps, totalCount);
        }

        private static IEnumerable<SearchWhatsAppOutput> ListWhatsAppToListOutput(List<WhatsApp> whatsApp, int totalCount)
        {
            var output = new List<SearchWhatsAppOutput>();

            foreach (var item in whatsApp)
                output.Add(new SearchWhatsAppOutput(item.WhatsAppId, item.Nome, item.Numero_Fone, item.Ativo, totalCount));

            return output;
        }

        private static SearchWhatsAppOutput WhatsAppToOutput(WhatsApp whatsApp)
        {
            var output = new SearchWhatsAppOutput(whatsApp.WhatsAppId, whatsApp.Nome, whatsApp.Numero_Fone, whatsApp.Ativo, 1);
            return output;
        }

        public async Task<bool> AddWhatsAppNumber(AddWhatsAppInput input)
        {
            var whatsApp = new WhatsApp() { Nome = input.Nome, Numero_Fone = input.NumeroFone };

            _dataContext.WhatsApp.Add(whatsApp);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateWhatsAppNumber(UpdateWhatsAppInput input)
        {
            WhatsApp? dbWhatsApp = await _dataContext.WhatsApp.FindAsync(input.Id);

            if (dbWhatsApp is null)
                return false;

            dbWhatsApp.Update(input.Nome, input.NumeroFone);

            var result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteWhatsAppNumber(int id)
        {
            WhatsApp? dbWhatsApp = await _dataContext.WhatsApp.FindAsync(id);

            if (dbWhatsApp is null)
                return false;

            dbWhatsApp.Delete();

            var result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }
    }
}

using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.UseCases.EventCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.EventCase
{
    public class DeleteEvent : IDeleteEvent
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<DeleteEvent> _logger;

        public DeleteEvent(IEventRepository eventRepository, ILogger<DeleteEvent> logger)
        {
            _eventRepository = eventRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(int id)
        {
            try
            {
                var result = await _eventRepository.DeleteEvent(id);

                if (result)
                {
                    _logger.LogInformation("Evento com ID {Id} deletado com sucesso.", id);
                }
                else
                {
                    _logger.LogWarning("Falha ao deletar evento com ID {Id}.", id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar evento com ID {Id}.", id);
                return false;
            }
        }
    }
}

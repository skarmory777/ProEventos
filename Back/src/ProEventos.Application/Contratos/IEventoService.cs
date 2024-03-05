using System;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(Guid userId, EventoDto Model);
        
        Task<EventoDto> UpdateEvento(Guid userId, int eventoId, EventoDto Model);

        Task<bool> DeleteEvento(Guid userId, int eventoId);

        Task<EventoDto[]> GetAllEventosAsync(Guid userId, bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync(Guid userId, string tema, bool includePalestrantes = false);        
        Task<EventoDto> GetEventoByIdAsync(Guid userId, int eventoId, bool includePalestrantes = false);        
    }
}
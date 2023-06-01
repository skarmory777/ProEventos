using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(EventoDto Model);
        
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto Model);

        Task<bool> DeleteEvento(int eventoId);

        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);        
        Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);        
    }
}
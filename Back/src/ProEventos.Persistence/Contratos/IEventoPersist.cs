using System;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllEventosByTemaAsync(Guid userId, string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(Guid userId, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(Guid userId, int eventoId, bool includePalestrantes = false);
    }
}
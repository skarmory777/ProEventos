using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;    
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper _mapper;

        public EventoService(IGeralPersist geralPersist, 
                             IEventoPersist eventoPersist,
                             IMapper mapper)
        {            
            this._eventoPersist = eventoPersist;
            this._geralPersist = geralPersist;
            this._mapper = mapper;
        }

        public async Task<EventoDto> AddEventos(Guid userId, EventoDto model)
        {
            try
            {
                model.UserId= userId;
                var evento = _mapper.Map<Evento>(model);
                 evento.User.Id= userId;
                 _geralPersist.Add<Evento>(evento);
                 if (await _geralPersist.SaveChangesAsync()){
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                 }
                 return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }

        public async Task<EventoDto> UpdateEvento(Guid userId, int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) throw new Exception("Evento não encontrado para edição.");

                model.Id = evento.Id;
                model.UserId = userId;

                _mapper.Map(model, evento);
                _geralPersist.Update<Evento>(evento);

                if (await _geralPersist.SaveChangesAsync()){
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }           
                return null;    
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }        

        public async Task<bool> DeleteEvento(Guid userId, int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) throw new Exception("Evento não encontrado para exclusão.");

                _geralPersist.Delete<Evento>(evento);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }

        public async Task<EventoDto[]> GetAllEventosAsync(Guid userId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(userId, includePalestrantes); 
                if (eventos == null) return null;

                var resultado = this._mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(Guid userId, string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(userId, tema, includePalestrantes); 
                if (eventos == null) return null;

                var resultado = this._mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(Guid userId, int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, includePalestrantes); 
                if (eventos == null) return null;

                var resultado = this._mapper.Map<EventoDto>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}
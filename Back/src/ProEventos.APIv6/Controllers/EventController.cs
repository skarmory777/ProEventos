using Microsoft.AspNetCore.Mvc;
using ProEventos.APIv6.Models;

namespace ProEventos.APIv6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    public IEnumerable<Event> _events = new Event[] {
        new Event() {
            EventId = 1,
            Tema = "Angular 11 e .Net",
            Local = "Rio de Janeiro",
            Lote = "1º Lote",
            QtdPessoas = 250,
            EventDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
            imagemURL = "foto.png"
        },
        new Event() {
            EventId = 2,
            Tema = "Angular  e Suas Novidades",
            Local = "São Paulo",
            Lote = "2º Lote",
            QtdPessoas = 350,
            EventDate = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"),
            imagemURL = "foto1.png"
        }            
    };

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<EventController> _logger;

    public EventController(ILogger<EventController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetEvent")]
    public IEnumerable<Event> Get()
    {
        return _events;
    }

    [HttpGet("id")]
    public IEnumerable<Event> GetById(int id)
    {
        return _events.Where(events => events.EventId == id);
    }    

    [HttpPost]
    public string Post()
    {
        return "Exemplo de Post";
    }

    [HttpPut]
    public string Put(int id)
    {
        return "Exemplo de Put";
    }
    [HttpDelete]
    public string Delete(int id)
    {
        return "Exemplo de Delete";
    }       

}

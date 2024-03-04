﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;


namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class EventosController : ControllerBase
  {
    private readonly IEventoService _eventoService;
    private readonly IWebHostEnvironment _hostEnvironment;

    public EventosController(IEventoService eventoService, IWebHostEnvironment hostEnvironment)
    {
      _eventoService = eventoService;
      _hostEnvironment = hostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var eventos = await _eventoService.GetAllEventosAsync(true);
        if (eventos == null) return NoContent();
        return Ok(eventos);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
      }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      try
      {
        var eventos = await _eventoService.GetEventoByIdAsync(id, true);
        if (eventos == null) return NoContent();
        return Ok(eventos);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
      }
    }

    [HttpGet("{tema}/tema")]
    public async Task<IActionResult> GetByTema(string tema)
    {
      try
      {
        var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
        if (eventos == null) return NoContent();
        return Ok(eventos);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
      }
    }

    [HttpPost("upload-image/{eventoId}")]
    public async Task<IActionResult> UploadImage(int eventoId)
    {
      try
      {
        var evento = await _eventoService.GetEventoByIdAsync(eventoId, true);
        if (evento == null) return NoContent();
        var file = Request.Form.Files[0];
        if (file.Length > 0)
        {
          DeleteImage(evento.ImagemURL);
          evento.ImagemURL = await SaveImage(file);
        }

        var EventoRetorno = await _eventoService.UpdateEvento(eventoId, evento);

        return Ok(EventoRetorno);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
      }
    }

    [NonAction]
    public async Task<string> SaveImage(IFormFile imageFile)
    {
      string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                        .Take(10)
                                        .ToArray()).Replace(' ', '-');
      imageName = $"{imageName}{DateTime.UtcNow:yyyymmddssfff}{Path.GetExtension(imageFile.FileName)}";

      var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);

      using (var fileStream = new FileStream(imagePath, FileMode.Create))
      {
        await imageFile.CopyToAsync(fileStream);
      }
      return imageName;
    }

    [NonAction]
    public void DeleteImage(string imageName)
    {
      var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);
      if(System.IO.File.Exists(imagePath))
        System.IO.File.Delete(imagePath);
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
      try
      {
        var eventos = await _eventoService.AddEventos(model);
        if (eventos == null) return NoContent();
        return Ok(eventos);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventoDto model)
    {
      try
      {
        var eventos = await _eventoService.UpdateEvento(id, model);
        if (eventos == null) return NoContent();
        return Ok(eventos);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao tentar alterar eventos. Erro: {ex.Message}");
      }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var evento = await _eventoService.GetEventoByIdAsync(id, true);
        if (evento == null) return NoContent();

        if (await _eventoService.DeleteEvento(id))
        {
          DeleteImage(evento.ImagemURL);
          return Ok(new { message = "Deletado" });
        }
        else
        {
          throw new Exception("Ocorreu um problema não específico ao tentar deletar Evento.");
        }
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError,
                               $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
      }
    }
  }
}

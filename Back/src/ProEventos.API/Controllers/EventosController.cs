using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync();
                if(eventos is null) return NotFound("Nenhum evento encontrado.");
                return Ok(eventos);
            }
            catch (Exception ex)
            {              
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id);
                if(evento is null) return NotFound("Não foi encontrado um evento com este id.");
                return Ok(evento);
            }
            catch (Exception ex)
            {              
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema);
                if(eventos is null) return NotFound("Não foi encontrado nenhum evento com este tema.");
                return Ok(eventos);
            }
            catch (Exception ex)
            {              
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if(evento is null) return BadRequest("Erro ao tentar adicionar o evento.");
                return Ok(evento);
            }
            catch (Exception ex)
            {              
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if(evento is null) return BadRequest("Erro ao tentar adicionar o evento.");
                return Ok(evento);
            }
            catch (Exception ex)
            {              
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar o evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(await _eventoService.DeleteEvento(id)) return Ok("Evento deletado.");
                else return BadRequest("Erro ao tentar deletar o evento.");
            }
            catch (Exception ex)
            {              
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o evento. Erro: {ex.Message}");
            }
        }

    }
}

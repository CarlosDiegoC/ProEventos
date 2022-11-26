using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Persistence;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly ProEventosContext _context;
        public EventosController(ProEventosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;
        }

        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return _context.Eventos.FirstOrDefault(evento => evento.Id == id);
        }

        [HttpPost]
        public Evento Post(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
            return evento;
        }

        [HttpPut("{id}")]
        public Evento Put(int id, Evento evento)
        {
            _context.Eventos.Update(evento);
            _context.SaveChanges();
            return evento;
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var evento = _context.Eventos.FirstOrDefault(evento => evento.Id == id);
            _context.Eventos.Remove(evento);
            _context.SaveChanges();
            return "Evento Deletado!";
        }

    }
}

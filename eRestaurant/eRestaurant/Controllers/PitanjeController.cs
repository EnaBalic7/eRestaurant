using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class PitanjeController:ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PitanjeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Pitanja
                .OrderByDescending(s => s.PitanjeID)
                .Include(k=>k.Korisnik.Osoba)
                .Include(r=>r.Radnik)
                .Take(100);
            return Ok(data.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.Pitanja.FirstOrDefault(s => s.PitanjeID == id));
        }

        [HttpPost]
        public Pitanje Add([FromBody] PitanjeAddViewModel x)
        {
            var newPitanje = new Pitanje
            {
                Opis = x.opis,
                Odgovor = x.odgovor,
                Status=x.status,
                Datum=x.datum,
                RadnikID = x.radnikID,
                KorisnikID = x.korisnikID
            };

            _dbContext.Add(newPitanje);
            _dbContext.SaveChanges();
            return newPitanje;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] PitanjeAddViewModel x)
        {
            Pitanje? pitanje = _dbContext.Pitanja.FirstOrDefault(s => s.PitanjeID == id);
            if (pitanje == null)
                return BadRequest("pogresan ID");
            pitanje.Opis = x.opis;
            pitanje.Odgovor = x.odgovor;
            pitanje.Datum = x.datum;
            pitanje.Status = x.status;

            _dbContext.SaveChanges();
            return Get(id);
        }



        [HttpPost("{id}")]
        public ActionResult Answer(int id, [FromBody] PitanjeAddViewModel x)
        {
            Pitanje? pitanje = _dbContext.Pitanja.FirstOrDefault(s => s.PitanjeID == id);
            if (pitanje == null)
                return BadRequest("pogresan ID");

            pitanje.Odgovor = x.odgovor;

            _dbContext.SaveChanges();
            return Get(id);
        }

        [HttpPost("{id}")]
        public ActionResult  Delete(int id)
        {
            Pitanje? pitanje = _dbContext.Pitanja.Find(id);
            if (pitanje == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(pitanje);
            _dbContext.SaveChanges();

            return Ok(pitanje);
        }
    }
}

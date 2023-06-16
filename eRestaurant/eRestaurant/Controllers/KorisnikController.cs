using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController:ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorisnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public Korisnik Add([FromBody] KorisnikAddViewModel x)
        {
            var newUser = new Korisnik
            {
                TipKorisnika=x.tipKorisnika,
                OsobaID=x.osobaID
            };

            _dbContext.Add(newUser);
            _dbContext.SaveChanges();
            return newUser;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Korisnici
                .OrderBy(s => s.KorisnikID)
                .Include(o=>o.Osoba)
                .Take(100);
            return Ok(data.ToList());
        }


        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Korisnik? korisnik = _dbContext.Korisnici.Find(id);

            if (korisnik == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(korisnik);

            _dbContext.SaveChanges();
            return Ok(korisnik);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] KorisnikAddViewModel x)
        {
            Korisnik? korisnik = _dbContext.Korisnici.FirstOrDefault(s => s.KorisnikID == id);

            if(korisnik==null)
                return BadRequest("pogresan ID");

            korisnik.TipKorisnika = x.tipKorisnika;

            _dbContext.SaveChanges();
            return Get(id);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.Korisnici.FirstOrDefault(s => s.KorisnikID == id));
        }
    }
}

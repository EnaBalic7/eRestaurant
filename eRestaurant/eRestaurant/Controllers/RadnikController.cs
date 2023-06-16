using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RadnikController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RadnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public List<Radnik> GetAll(string? naziv_radnika)
        {
            var data = _dbContext.Radnici
                .Where(x => naziv_radnika == null || (x.Osoba.Ime.Contains(naziv_radnika))).OrderByDescending(s => s.RadnikID)
                .Include(o=>o.Osoba);
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Add([FromBody] RadnikAddVM x)
        {
            var newRadnik = new Radnik
            {
                DatumZaposlenja = x.DatumZaposlenja,
                DatumZavrsetkaRadnogOdnosa = x.DatumZavrsetkaRadnogOdnosa,
                Zvanje = x.Zvanje,
                JMBG = x.JMBG,
                Spol = x.Spol,
                Pozicija = x.Pozicija,
                OsobaID = GetLastAddedOsobaID()
            };

            _dbContext.Add(newRadnik);
            _dbContext.SaveChanges();
            return Ok(newRadnik);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] RadnikUpdateVM x)
        {
            Radnik? radnik = _dbContext.Radnici.Include(s => s.Osoba).FirstOrDefault(s => s.RadnikID == id);

            if (radnik == null)
                return BadRequest("pogresan ID");

            radnik.DatumZaposlenja = x.DatumZaposlenja;
            radnik.DatumZavrsetkaRadnogOdnosa = x.DatumZavrsetkaRadnogOdnosa;
            radnik.Zvanje = x.Zvanje;
            radnik.JMBG = x.JMBG;
            radnik.Spol = x.Spol;
            radnik.Pozicija = x.Pozicija;
            radnik.OsobaID = x.OsobaID;


            _dbContext.SaveChanges();
            return Ok();
        }


        [HttpPost]
        public ActionResult<Radnik> Delete([FromBody] Radnik x)
        {
            Radnik radnik = _dbContext.Radnici.Find(x.RadnikID);
            _dbContext.Remove(radnik);

            _dbContext.SaveChanges();
            return Ok(radnik);
        }

        [HttpGet]
        public int GetLastAddedOsobaID()
        {
            var id = _dbContext.Osobe.OrderByDescending(s => s.OsobaID).
                Select(s => s.OsobaID).FirstOrDefault();

            return id;
        }

    }
}

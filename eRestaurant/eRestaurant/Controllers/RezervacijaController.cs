using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class RezervacijaController:ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RezervacijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Rezervacije
                .OrderByDescending(s => s.RezervacijaID)
                .Select(s => new RezervacijaGetAllViewModel()
                {
                    id = s.RezervacijaID,
                    korisnikId=s.KorisnikID,
                    brojStola=s.BrojStola,
                    brojOsoba=s.BrojOsoba,
                    datumRezervacije=s.DatumRezervacije.ToString(),
                    status=s.Status
                })
                .Take(100);
            return Ok(data.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.Rezervacije.FirstOrDefault(s => s.RezervacijaID == id)); ;
        }

        [HttpPost]
        public Rezervacija Add([FromBody] RezervacijaAddViewModel x)
        {
            var newReservation = new Rezervacija
            {   
                DatumRezervacije=x.datumRezervacije,
                BrojOsoba=x.brojOsoba,
                BrojStola=x.brojStola,
                Napomena=x.napomena,
                Status=x.status,
                KorisnikID=x.korisnikID
            };

            _dbContext.Add(newReservation);
            _dbContext.SaveChanges();
            return newReservation;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] RezervacijaAddViewModel x)
        {
            Rezervacija? rezervacija = _dbContext.Rezervacije.FirstOrDefault(s => s.RezervacijaID == id);

            if (rezervacija == null)
                return BadRequest("pogresan ID");

            rezervacija.DatumRezervacije = x.datumRezervacije;
            rezervacija.BrojOsoba = x.brojOsoba;
            rezervacija.BrojStola = x.brojStola;
            rezervacija.Napomena = x.napomena;
            rezervacija.Status = x.status;

            _dbContext.SaveChanges();
            return Get(id);
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Rezervacija? rezervacija = _dbContext.Rezervacije.Find(id);

            if (rezervacija == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(rezervacija);

            _dbContext.SaveChanges();
            return Ok(rezervacija);
        }
    }
}
                                                                                                                                                                         
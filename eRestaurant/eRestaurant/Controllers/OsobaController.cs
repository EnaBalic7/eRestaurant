using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OsobaController:ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OsobaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public Osoba Add([FromBody] OsobaAddViewModel x)
        {
            var newPerson = new Osoba
            {
                Ime=x.ime,
                Prezime=x.prezime,
                DatumRodenja=x.datumRodenja,
                BrojTelefona=x.brojTelefona,
                Username=x.username,
                Password=x.password,
                Email=x.email,
                Slika=x.slika
            };

            _dbContext.Add(newPerson);
            _dbContext.SaveChanges();
            return newPerson;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Osobe
                .OrderBy(s => s.OsobaID)
                .Select(s => new OsobaGetAllViewModel()
                {
                    id = s.OsobaID,
                    ime=s.Ime,
                    prezime=s.Prezime,
                    datumRodenja=s.DatumRodenja,
                    brojTelefona=s.BrojTelefona,
                    username=s.Username,
                    password=s.Password,
                    email=s.Email,
                    slika=s.Slika
                })
                .Take(100);
            return Ok(data.ToList());
        }


        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] OsobaUpdateVM x)
        {
            Osoba? osoba = _dbContext.Osobe.FirstOrDefault(s => s.OsobaID == id);

            if (osoba == null)
                return BadRequest("pogresan ID");

            osoba.Ime = x.ime;
            osoba.Prezime = x.prezime;
            osoba.DatumRodenja = x.datumRodenja;
            osoba.BrojTelefona = x.brojTelefona;
            osoba.Email = x.email;
            osoba.Username = x.username;
            osoba.Password = x.password;
            osoba.Slika = x.slika;

            _dbContext.SaveChanges();
            return Ok();
        }
    }
}

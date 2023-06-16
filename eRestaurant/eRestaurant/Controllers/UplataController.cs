using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UplataController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UplataController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Uplate
                .OrderBy(s => s.UplataID)
                .Select(s => new UplataGetAllViewModel()
                {
                    id = s.UplataID,
                    nacinPlacanja = s.NacinPlacanja,
                    brojKartice = s.BrojKartice,
                    iznosZaUplatu = s.IznosZaUplatu,
                    datum = s.Datum.ToString()
                })
                .Take(100);
            return Ok(data.ToList());
        }

        [HttpPost]
        public Uplata Add([FromBody] UplataAddViewModel x)
        {
            var newUplata = new Uplata
            {
                NacinPlacanja = x.nacinPlacanja,
                BrojKartice = x.brojKartice,
                IznosZaUplatu = x.iznosZaUplatu,
                Datum = x.datum
            };

            _dbContext.Add(newUplata);
            _dbContext.SaveChanges();
            return newUplata;
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Uplata? uplata = _dbContext.Uplate.Find(id);

            if (uplata == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(uplata);
            _dbContext.SaveChanges();
            return Ok(uplata);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] UplataAddViewModel x)
        {
            Uplata? uplata = _dbContext.Uplate.FirstOrDefault(s => s.UplataID == id);

            if (uplata == null)
                return BadRequest("pogresan ID");

            uplata.NacinPlacanja = x.nacinPlacanja;
            uplata.BrojKartice = x.brojKartice;
            uplata.IznosZaUplatu = x.iznosZaUplatu;
            uplata.Datum = x.datum;

            _dbContext.SaveChanges();
            return Get(id);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.Uplate.FirstOrDefault(s => s.UplataID == id));
        }
    }
}

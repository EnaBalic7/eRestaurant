using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdreseController:ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AdreseController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public KorisnickeAdrese Add([FromBody] KorisnickeAdreseAddViewModel x)
        {
            var newAddress = new KorisnickeAdrese
            {
                NazivAdreseStanovanja = x.nazivAdreseStanovanja,
                BrojUlice = x.brojUlice,
                PostanskiBroj = x.postanskiBroj,
                KorisnikID = x.korisnikID
            };

            _dbContext.Add(newAddress);
            _dbContext.SaveChanges();
            return newAddress;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.KorisnickeAdrese
                .OrderBy(s => s.KorickaAdresaID)
                .Select(s => new KorisnickeAdreseGetAllViewModel()
                {
                    id = s.KorickaAdresaID,
                    nazivAdreseStanovanja = s.NazivAdreseStanovanja,
                    brojUlice = s.BrojUlice,
                    postanskiBroj = s.PostanskiBroj,
                    korisnikID = s.KorisnikID
                })
                .Take(100);
            return Ok(data.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.KorisnickeAdrese.FirstOrDefault(s => s.KorickaAdresaID == id));
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            KorisnickeAdrese? adresa = _dbContext.KorisnickeAdrese.Find(id);

            if(adresa==null || id == 1)
            {
                return BadRequest("pogresan ID");
            }

            _dbContext.Remove(adresa);
            _dbContext.SaveChanges();
            return Ok(adresa);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] KorisnickeAdreseAddViewModel x)
        {
            KorisnickeAdrese? adresa = _dbContext.KorisnickeAdrese.FirstOrDefault(s => s.KorickaAdresaID == id);

            if (adresa == null)
                return BadRequest("pogresan ID");
            adresa.NazivAdreseStanovanja = x.nazivAdreseStanovanja;
            adresa.BrojUlice = x.brojUlice;
            adresa.PostanskiBroj = x.postanskiBroj;

            _dbContext.SaveChanges();

            return Get(id);
        }

    }
}

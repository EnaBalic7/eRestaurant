using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eRestaurant.Helper;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProizvodController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProizvodController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAll(string? naziv_proizvoda)
        {
            var data = _dbContext.Proizvodi
                .Where(x => naziv_proizvoda == null || (x.NazivProizvoda.Contains(naziv_proizvoda)))
                .Include(s=>s.ProizvodiKategorije)
                .OrderByDescending(s => s.ProizvodID)
                .Select(p => new ProizvodGetAllVM()
                {
                    ProizvodID = p.ProizvodID,
                    NazivProizvoda = p.NazivProizvoda,
                    PocetnaCijena = p.PocetnaCijena,
                    Opis = p.Opis,
                    Recenzija = p.Recenzija,
                    JedinicaMjere = p.JedinicaMjere,
                    ProizvodiKategorijeID = p.ProizvodiKategorijeID,
                    SlikaPostojeca = p.Slika
                }).ToList();

            return Ok(data);
        }


        [HttpGet("{id}")]

        public ActionResult Get(int id)
        {
            return Ok(_dbContext.Proizvodi.Include(s => s.ProizvodiKategorije).FirstOrDefault(s => s.ProizvodID == id)); ;
        }

        [HttpPost]
        public ActionResult Add([FromBody] ProizvodAddVM x)
        {
            var newProizvod = new Proizvod
            {
                NazivProizvoda = x.NazivProizvoda,
                PocetnaCijena = x.PocetnaCijena,
                Opis = x.Opis,
                Recenzija = x.Recenzija,
                Slika = x.SlikaNovaBase64.ParsirajBase64(),
                JedinicaMjere = x.JedinicaMjere,
                ProizvodiKategorijeID = x.ProizvodiKategorijeID
            };

            _dbContext.Add(newProizvod);
            _dbContext.SaveChanges();
            return Get(newProizvod.ProizvodID);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] ProizvodUpdateVM x)
        {
            Proizvod? proizvod = _dbContext.Proizvodi.Include(s => s.ProizvodiKategorije).FirstOrDefault(s => s.ProizvodID == id);

            if (proizvod == null)
                return BadRequest("pogresan ID");

            proizvod.NazivProizvoda = x.NazivProizvoda;
            proizvod.PocetnaCijena = x.PocetnaCijena;
            proizvod.Opis = x.Opis;
            proizvod.Recenzija = x.Recenzija;
            proizvod.Slika = x.SlikaNovaBase64.ParsirajBase64();
            proizvod.JedinicaMjere = x.JedinicaMjere;
            proizvod.ProizvodiKategorijeID = x.ProizvodiKategorijeID;

            _dbContext.SaveChanges();
            return Get(id);
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Proizvod? proizvod = _dbContext.Proizvodi.Find(id);

            if (proizvod == null || id == 0)
                return BadRequest("pogresan ID");

            _dbContext.Remove(proizvod);

            _dbContext.SaveChanges();
            return Ok(proizvod);
        }

        [HttpGet("{id}")]
        public ActionResult GetSlika(int id)
        {
            byte[] bajtovi_slike = _dbContext.Proizvodi.Find(id).Slika;
            if(bajtovi_slike.Length == 0 || bajtovi_slike == null)
            {
                bajtovi_slike = System.IO.File.ReadAllBytes("Images/image.png");
            }

            return File(bajtovi_slike,"image/png");
        }
    }
}

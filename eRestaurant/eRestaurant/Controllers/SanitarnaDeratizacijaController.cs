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
    public class SanitarnaDeratizacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SanitarnaDeratizacijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAll(DateTime? datum1, DateTime? datum2, int pageNumber = 1, int pageSize = 5)
        {
            if(datum1 != null && datum2 != null)
            {
                IQueryable<SanitarnaDeratizacija> dataFiltered = _dbContext.SanitarneDeratizacije
                    .Where(d=>d.DatumInspekcije >= datum1 && d.DatumInspekcije.Date <= datum2.Value.Date)
               .Include(s => s.Radnik)
               .OrderByDescending(s => s.DatumInspekcije);

               return Ok(PagedList<SanitarnaDeratizacija>.Create(dataFiltered, pageNumber, pageSize));
            }

            IQueryable <SanitarnaDeratizacija> data = _dbContext.SanitarneDeratizacije
                .Include(s => s.Radnik)
                .OrderByDescending(s => s.DatumInspekcije);

            return Ok(PagedList<SanitarnaDeratizacija>.Create(data, pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Add([FromBody] SanitarnaAddVM x)
        {
            var newInspekcija = new SanitarnaDeratizacija();

            newInspekcija.DatumInspekcije = x.DatumInspekcije;
            newInspekcija.BrojUgovora = x.BrojUgovora;
            newInspekcija.DatumOvjere = x.DatumOvjere;
            newInspekcija.DodatnaNapomena = x.DodatnaNapomena;
            newInspekcija.RadnikID = x.RadnikID;
            newInspekcija.PDF = x.PDF.ParsirajBase64();

            if (!string.IsNullOrEmpty(x.PDF))
            {
                newInspekcija.PrilozenPDF = true;
            }

            _dbContext.Add(newInspekcija);
            _dbContext.SaveChanges();
            return Ok(newInspekcija);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] SanitarnaUpdateVM x)
        {
            SanitarnaDeratizacija? inspekcija = _dbContext.SanitarneDeratizacije.Include(s => s.Radnik).FirstOrDefault(s => s.SanitarnaDeratizacijaID == id);

            if (inspekcija == null)
                return BadRequest("pogresan ID");

            inspekcija.DatumInspekcije = x.DatumInspekcije;
            inspekcija.BrojUgovora = x.BrojUgovora;
            inspekcija.DatumOvjere = x.DatumOvjere;
            inspekcija.DodatnaNapomena = x.DodatnaNapomena;
            inspekcija.RadnikID = x.RadnikID;
            inspekcija.PDF = x.PDF.ParsirajBase64();

            if (!string.IsNullOrEmpty(x.PDF))
            {
                inspekcija.PrilozenPDF = true;
            }
          

            _dbContext.SaveChanges();
            return Ok(inspekcija);
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            SanitarnaDeratizacija? inspekcija = _dbContext.SanitarneDeratizacije.Find(id);

            if (inspekcija == null || id == 0)
                return BadRequest("pogresan ID");

            _dbContext.Remove(inspekcija);

            _dbContext.SaveChanges();
            return Ok(inspekcija);
        }

        [HttpGet("{id}")]
        public ActionResult GetPDF(int id)
        {
            byte[] bajtovi_pdf = _dbContext.SanitarneDeratizacije.Find(id).PDF;

            return new FileContentResult(bajtovi_pdf,"application/pdf");
        }
    }
}

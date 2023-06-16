using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DobavljacController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DobavljacController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public List<DobavljacGetAllVM> GetAll(string? naziv_dobavljaca)
        {
            var data = _dbContext.Dobavljaci
                .Where(x => naziv_dobavljaca == null || (x.NazivKompanije.Contains(naziv_dobavljaca))).OrderBy(s => s.DobavljacID)
                .Select(x => new DobavljacGetAllVM
                {
                    DobavljacID = x.DobavljacID,
                    NazivKompanije = x.NazivKompanije,
                    BrojUgovora = x.BrojUgovora,
                    KontaktOsoba = x.KontaktOsoba,
                    BrojTelefona = x.BrojTelefona
                });
            return data.Take(100).ToList();
        }


        [HttpPost]
        public ActionResult Add([FromBody] DobavljacAddVM x)
        {
            var newDobavljac = new Dobavljac
            {
                NazivKompanije = x.NazivKompanije,
                BrojUgovora = x.BrojUgovora,
                KontaktOsoba = x.KontaktOsoba,
                BrojTelefona = x.BrojTelefona
            };

            _dbContext.Add(newDobavljac);
            _dbContext.SaveChanges();
            return Ok(newDobavljac.DobavljacID);
        }


        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Dobavljac? dobavljac = _dbContext.Dobavljaci.Find(id);

            if (dobavljac == null || id == 0)
                return BadRequest("pogresan ID");

            _dbContext.Remove(dobavljac);

            _dbContext.SaveChanges();
            return Ok(dobavljac);
        }
    }
}

using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SastojciController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SastojciController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public List<Sastojak> GetAll(string? naziv_sastojka)
        {
            var data = _dbContext.Sastojci
                .Where(x => naziv_sastojka == null || (x.Naziv.Contains(naziv_sastojka))).OrderByDescending(s => s.SastojakID)
                .Include(s => s.SastojciKategorije)
                .Include(d=>d.Dobavljac);
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Add([FromBody] SastojakAddVM x)
        {
            var newSastojak = new Sastojak
            {
                Naziv = x.Naziv,
                KolicinaNaStanju = x.KolicinaNaStanju,
                DobavljacID =x.DobavljacID,
                SastojciKategorijeID=x.SastojciKategorijeID
            };

            _dbContext.Add(newSastojak);
            _dbContext.SaveChanges();
            return Ok(newSastojak);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] SastojakUpdateVM x)
        {
            Sastojak? sastojak = _dbContext.Sastojci.Include(s => s.SastojciKategorije).FirstOrDefault(s => s.SastojakID == id);

            if (sastojak == null)
                return BadRequest("pogresan ID");

            sastojak.Naziv = x.Naziv;
            sastojak.KolicinaNaStanju = x.KolicinaNaStanju;
            
            sastojak.DobavljacID = x.DobavljacID;
            sastojak.SastojciKategorijeID = x.SastojciKategorijeID;

            _dbContext.SaveChanges();
            return Ok();
        }


        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Sastojak? sastojak = _dbContext.Sastojci.Find(id);

            if (sastojak == null || id == 0)
                return BadRequest("pogresan ID");

            _dbContext.Remove(sastojak);

            _dbContext.SaveChanges();
            return Ok(sastojak);
        }
    }
}

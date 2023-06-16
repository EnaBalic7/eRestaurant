using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SastojciKategorijeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SastojciKategorijeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public List<SastojciKategorijeGetAllVM> GetAll(string? naziv_kategorije)
        {
            var data = _dbContext.SastojciKategorije
                .Where(x => naziv_kategorije == null || (x.Naziv.Contains(naziv_kategorije))).OrderBy(s => s.SastojciKategorijeID)
                .Select(x => new SastojciKategorijeGetAllVM
                {
                    SastojciKategorijeID = x.SastojciKategorijeID,
                    Naziv = x.Naziv
                });
            return data.Take(100).ToList();
        }


        [HttpPost]
        public ActionResult Add([FromBody] SastojciKategorijeAddVM x)
        {
            var newKategorija = new SastojciKategorije
            {
                Naziv = x.Naziv
            };

            _dbContext.Add(newKategorija);
            _dbContext.SaveChanges();
            return Ok(newKategorija);
        }


        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            SastojciKategorije? kategorija = _dbContext.SastojciKategorije.Find(id);

            if (kategorija == null || id == 0)
                return BadRequest("pogresan ID");

            _dbContext.Remove(kategorija);

            _dbContext.SaveChanges();
            return Ok(kategorija);
        }

    }
}

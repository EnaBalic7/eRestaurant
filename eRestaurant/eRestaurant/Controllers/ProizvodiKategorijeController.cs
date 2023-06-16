using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProizvodiKategorijeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProizvodiKategorijeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public List<ProizvodiKategorijeGetAllVM> GetAll(string? naziv_kategorije)
        {
            var data = _dbContext.ProizvodiKategorije
                .Where(x => naziv_kategorije == null || (x.NazivKategorije.Contains(naziv_kategorije)))
                .Select(x => new ProizvodiKategorijeGetAllVM
                {
                    ProizvodiKategorijeID=x.ProizvodiKategorijeID,
                    NazivKategorije=x.NazivKategorije
                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Add([FromBody] ProizvodiKategorijeAddVM x)
        {
            var newProizvodKategorija = new ProizvodiKategorije
            {
                NazivKategorije=x.NazivKategorije
            };
            
            _dbContext.Add(newProizvodKategorija);
            _dbContext.SaveChanges();
            return Ok(newProizvodKategorija);
        }

    }
}

using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KuponController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KuponController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public List<Kupon> GetAll()
        {
            var kuponi = _dbContext.Kuponi.Where(d => d.DatumDeaktivacije < DateTime.Now).ToList();
            kuponi.ForEach(x => x.Deaktiviran = true);

            var kuponi2 = _dbContext.Kuponi.Where(d => d.DatumDeaktivacije > DateTime.Now).ToList();
            kuponi2.ForEach(x => x.Deaktiviran = false);

            _dbContext.SaveChanges();

            var data = _dbContext.Kuponi.OrderByDescending(s => s.KuponID);
            return data.Take(100).ToList();
        }


        [HttpPost]
        public ActionResult Add([FromBody] KuponAddVM x)
        {
            if (x.Popust < 0)
                return BadRequest("Popust ne može biti negativan.");
            else if (x.Popust == 0)
                return BadRequest("Popust ne može biti jednak nuli.");

            var newKupon = new Kupon
            {
                DatumAktivacije=x.DatumAktivacije,
                DatumDeaktivacije=x.DatumDeaktivacije,
                Popust=x.Popust
            };

            _dbContext.Add(newKupon);
            _dbContext.SaveChanges();
            return Ok(newKupon);
        }

        [HttpPost]
        public ActionResult Update([FromBody] KuponUpdateVM x)
        {
            Kupon? kupon = _dbContext.Kuponi.FirstOrDefault(s => s.KuponID == x.KuponID);

            if (kupon == null)
                return BadRequest("pogresan ID");

            kupon.DatumDeaktivacije= x.DatumDeaktivacije;

            _dbContext.SaveChanges();
            return Ok(kupon);
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Kupon? kupon = _dbContext.Kuponi.Find(id);

            if (kupon == null || id == 0)
                return BadRequest("pogresan ID");

            _dbContext.Remove(kupon);

            _dbContext.SaveChanges();
            return Ok(kupon);
        }
    }
}

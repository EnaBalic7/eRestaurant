using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Linq;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IzvjestajController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public IzvjestajController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAll(DateTime? datum1, DateTime? datum2)
        {
            var sveNarudzbe = _dbContext.Narudzbe
                .Select(y => new
                {
                    y.NarudzbaID,
                    y.DatumNarudzbe,
                    y.UkupnaCijenaSaPDV,
                    ukupnaZarada = _dbContext.Narudzbe.Where(d=>d.DatumNarudzbe==y.DatumNarudzbe).Sum(z => z.UkupnaCijenaSaPDV)
                }).ToList();

            if (datum1 != null && datum2!=null)
            {
                var narudzbeByDateFiltered = sveNarudzbe
                    .Where(x => x.DatumNarudzbe>=datum1 && x.DatumNarudzbe.Date<=datum2.Value.Date)
                    .OrderByDescending(d => d.DatumNarudzbe)
                    .GroupBy(d => d.DatumNarudzbe)
                    .ToList();

                if (narudzbeByDateFiltered == null)
                    return BadRequest("Filtered narudžbe su null.");

                    return Ok(narudzbeByDateFiltered);
            }


            var narudzbeByDate = sveNarudzbe
                   .OrderByDescending(d => d.DatumNarudzbe)
                   .GroupBy(d => d.DatumNarudzbe)
                   .ToList();

          
            if (narudzbeByDate == null)
                return BadRequest("Narudžbe su null.");

            return Ok(narudzbeByDate);
        }

    }
}

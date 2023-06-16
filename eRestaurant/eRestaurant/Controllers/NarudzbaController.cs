using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
   
    [ApiController]
    [Route("[controller]/[action]")]
   
        public class NarudzbaController : ControllerBase
        {
            private readonly ApplicationDbContext _dbContext;

            public NarudzbaController(ApplicationDbContext dbContext)
            {
                this._dbContext = dbContext;
            }

        [HttpGet]
        public ActionResult GetAll(bool? onlineOnly, bool? readyOnly)
        {
            if(readyOnly == null && onlineOnly != null && onlineOnly == true)
            {
                var onlineOrders = _dbContext.Narudzbe
                    .Where(s=>s.TipNarudzbe==true)
                    .OrderByDescending(x => x.NarudzbaID)
                    .Include(k=>k.Korisnik.Osoba)
                    .ToList();

                if (onlineOrders == null)
                    return BadRequest("Online narudžbe su null");

                return Ok(onlineOrders);
            }
            else if((onlineOnly != null && onlineOnly == true) && (readyOnly != null && readyOnly == true))
            {
                var onlineOrders = _dbContext.Narudzbe
                  .Where(s => s.TipNarudzbe == true && (s.StatusNarudzbe=="Prepared" || s.StatusNarudzbe == "Is being delivered" || s.StatusNarudzbe == "Delivered"))
                  .OrderByDescending(x => x.NarudzbaID)
                  .Include(k => k.Korisnik.Osoba)
                  .ToList();

                if (onlineOrders == null)
                    return BadRequest("Online narudžbe su null");

                return Ok(onlineOrders);
            }
            else if(onlineOnly==null && readyOnly != null && readyOnly == true)
            {
                var onlineOrders = _dbContext.Narudzbe
                 .Where(s => s.StatusNarudzbe == "Prepared" || s.StatusNarudzbe == "Is being delivered" || s.StatusNarudzbe == "Delivered")
                 .OrderByDescending(x => x.NarudzbaID)
                 .Include(k => k.Korisnik.Osoba)
                 .ToList();

                if (onlineOrders == null)
                    return BadRequest("Online narudžbe su null");

                return Ok(onlineOrders);
            }

            var data = _dbContext.Narudzbe
                .OrderBy(s => s.NarudzbaID)
                .Select(s => new NarudzbaGetAllVM()
                {
                    NarudzbaID = s.NarudzbaID,
                    Korisnik = s.Korisnik,
                    TipNarudzbe = s.TipNarudzbe,
                    UkupnaCijenaBezPopusta = s.UkupnaCijenaBezPopusta,
                    UkupnaCijena = s.UkupnaCijena,
                    UkupnaCijenaSaPDV = s.UkupnaCijenaSaPDV,
                    StatusNarudzbe = s.StatusNarudzbe,
                    DodatnaNapomena = s.DodatnaNapomena,
                    DatumNarudzbe = s.DatumNarudzbe,
                    PDV = s.PDV,
                    Popust = s.Popust,
                    Radnik = s.Radnik,
                    Uplata = s.Uplata,
                    Dostavljac = s.Dostavljac,
                    CijenaBezPDV = s.CijenaBezPDV
                 
                })
                .Take(100);
            return Ok(data.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.Narudzbe.FirstOrDefault(s => s.NarudzbaID == id));
        }

        [HttpPost]
        public Narudzba Add([FromBody] NarudzbaAddVM x)
        {
            var novaNarudzba = new Narudzba
            {           
                KorisnikID = x.KorisnikID,
                TipNarudzbe = x.TipNarudzbe,
                UkupnaCijenaBezPopusta = x.UkupnaCijenaBezPopusta,
                UkupnaCijena = x.UkupnaCijena,
                UkupnaCijenaSaPDV = x.UkupnaCijenaSaPDV,
                StatusNarudzbe = x.StatusNarudzbe,
                DodatnaNapomena = x.DodatnaNapomena,
                DatumNarudzbe = x.DatumNarudzbe,
                PDV = x.PDV,
                Popust = x.Popust,
                RadnikID = x.RadnikID,
                UplataID = x.UplataID,
                DostavljacID = x.DostavljacID,
                CijenaBezPDV = x.CijenaBezPDV
            };

            _dbContext.Add(novaNarudzba);
            _dbContext.SaveChanges();
            return novaNarudzba;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] NarudzbaUpdateVM x)
        {
            Narudzba? narudzba = _dbContext.Narudzbe.FirstOrDefault(s => s.NarudzbaID == id);
            if (narudzba == null)
                return BadRequest("pogresan ID");

            narudzba.TipNarudzbe = x.TipNarudzbe;
            narudzba.UkupnaCijenaBezPopusta = x.UkupnaCijenaBezPopusta;
            narudzba.Popust = x.Popust;
            narudzba.UkupnaCijena = x.UkupnaCijena;
            narudzba.StatusNarudzbe = x.StatusNarudzbe;
            narudzba.DodatnaNapomena = x.DodatnaNapomena;
            narudzba.DatumNarudzbe = x.DatumNarudzbe;
            narudzba.CijenaBezPDV = x.CijenaBezPDV;
            narudzba.PDV = x.PDV;
            narudzba.UkupnaCijenaSaPDV = x.UkupnaCijenaSaPDV;
            narudzba.KorisnikID = x.KorisnikID;
            narudzba.RadnikID = x.RadnikID;
            narudzba.DostavljacID = x.DostavljacID;
            narudzba.UplataID = x.UplataID;

            _dbContext.SaveChanges();
            return Get(id);
        }



        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Narudzba? narudzba = _dbContext.Narudzbe.Find(id);

            if (narudzba == null || id == 0)
                return BadRequest("pogresan ID");

            _dbContext.Remove(narudzba);

            _dbContext.SaveChanges();
            return Ok(narudzba);
        }


    }
}

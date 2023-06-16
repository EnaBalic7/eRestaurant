using eRestaurant.Data;
using eRestaurant.Models;
using eRestaurant.SignalR;
using eRestaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestirajSignalRController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<PorukeHub> _porukeHub;
        public TestirajSignalRController(ApplicationDbContext dbContext, IHubContext<PorukeHub> porukeHub)
        {
            this._dbContext = dbContext;
            _porukeHub = porukeHub;
        }

        [HttpGet]
        public async Task<ActionResult> PosaljiTrenutnoVrijeme()
        {
            string p = "Trenutno vrijeme je: " + DateTime.Now;
            await _porukeHub.Clients.All.SendAsync("slanje_poruke1", p);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> PosaljiPoruku(string p)
        {
            await _porukeHub.Clients.All.SendAsync("slanje_poruke2", p);
            return Ok();
        }


    }
}

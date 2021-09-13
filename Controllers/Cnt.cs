using CounterApp;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("cnt/{**slug}")]
    public class Cnt:ControllerBase
    {
        //some small changes, just to check a branch...
        private readonly CounterDbContext _db;
        public Cnt(CounterDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public string Get(string slug)
        {
            return _db.GetCounter( HttpContext.Request.Path.ToString() ).ToString();
        }

    }
}
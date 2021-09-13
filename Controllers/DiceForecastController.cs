using System.Collections.Generic;
using System.Linq;
using CounterApp;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("controller")]
    public class DiceForecastController:ControllerBase
    {
        private readonly CounterDbContext _db;

        public DiceForecastController(CounterDbContext counterDbContext)
        {
            _db = counterDbContext;
        }

        [HttpGet]
        public IEnumerable<DiceForecast> Get()
        {
            return Enumerable.Repeat(new DiceForecast(), 25).Select((forecast, i) =>
            {
                
                forecast.Value = i;
                forecast.Description = $"the value = {i.ToString()}";
                return forecast;
            } );
            
        }
    }
}
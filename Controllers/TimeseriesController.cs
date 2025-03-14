using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ExerciseLogApi.Models;
using Microsoft.Extensions.ObjectPool;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace timeseriesLog.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeseriesController : ControllerBase
    {
 
        private readonly Persistence _timeseriesRepository;

        public TimeseriesController(Persistence timeseriesRepository)
        {
            _timeseriesRepository = timeseriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timeseries>>> Get()
        {
            return Ok(_timeseriesRepository.Set<Timeseries>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Timeseries>> Get(int id)
        {
            var timeseries = await _timeseriesRepository.FindAsync<Timeseries>(id);
            if (timeseries == null)
            {
                return NotFound();
            }
            return Ok(timeseries);
        }
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<Timeseries>>> GetLatest(int number)
        {
            var latestTimeserie = _timeseriesRepository
                .Set<Timeseries>()
                .OrderByDescending(ts => ts.Timestamp)
                .Take(number); // Assuming there's a Timestamp property to order by

            if (latestTimeserie == null)
            {
                return NotFound("No timeseries found.");
            }

            return Ok(latestTimeserie);
        }

        [HttpPost]
        public async Task<ActionResult<Timeseries>> Post(Timeseries timeseries)
        {
            await _timeseriesRepository.AddAsync(timeseries);
            await _timeseriesRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = timeseries.Id }, timeseries);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DistinctDimensions : ControllerBase
    {
        private readonly Persistence _timeseriesRepository;

        public DistinctDimensions(Persistence timeseriesRepository)
        {
            _timeseriesRepository = timeseriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<String>>> GetUniqueDimensions()
        {
            var d = _timeseriesRepository.Timeseries
                .Select(ts => ts.Dimension)
                .Distinct()
                .ToList();
            return Ok(d);
        }
    }
}


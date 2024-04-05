using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ExerciseLogApi.Models;
using Microsoft.Extensions.ObjectPool;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace ExerciseLog.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
 
        private readonly Persistence _exerciseRepository;

        public ExerciseController(Persistence exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> Get()
        {
            return Ok(_exerciseRepository.Set<Exercise>());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> Get(int id)
        {
            var exercise = await _exerciseRepository.FindAsync<Exercise>(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> Post(Exercise exercise)
        {
            await _exerciseRepository.AddAsync(exercise);
            await _exerciseRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = exercise.Id }, exercise);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SessionController : ControllerBase
    {
        private readonly Persistence _sessionRepository;

        public SessionController(Persistence sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> Get()
        {
            return Ok(_sessionRepository.Set<Session>());

        }

        [HttpPost]
        public async Task<ActionResult<Session>> Post(Session session)
        {
            await _sessionRepository.AddAsync(session);
            await _sessionRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = session.Id }, session);
        }
    }
    // Endpoint for exercise sets
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SetController : ControllerBase
    {
        private readonly Persistence _setRepository;

        public SetController(Persistence setRepository)
        {
            _setRepository = setRepository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Set>> Get()
        {
            return Ok(_setRepository.Set<Set>());
        }
        
        [HttpGet("{exerciseId}")]
        public async Task<ActionResult<Exercise>> Get(int exerciseId)
        {
            // var sets = await _setRepository.FindAsync<Set>(exerciseId);
            var sets = _setRepository.Sets.Where(e => e.ExerciseId == exerciseId);

            if (sets == null)
            {
                return NotFound();
            }
            return Ok(sets);
        }

        [HttpPost]
        public async Task<ActionResult<Set>> Post(Set set)
        {
            await _setRepository.AddAsync(set);
            await _setRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = set.Id }, set);
        }
    }

}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ExerciseLogApi.Models;
using Microsoft.Extensions.ObjectPool;

namespace ExerciseLog.Api
{
    [Route("api/[controller]")]
    [ApiController]
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

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Put(int id, Exercise exercise)
        // {
        //     if (id != exercise.Id)
        //     {
        //         return BadRequest();
        //     }
        //     await _exerciseRepository.Update<Exercise>(exercise);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var exercise = await _exerciseRepository.Remove<Exercise>(id);
        //     if (exercise == null)
        //     {
        //         return NotFound();
        //     }
        //     await _exerciseRepository.DeleteExerciseAsync(exercise);
        //     return NoContent();
        // }
    }
    // Endpoint for exercise sets
    [Route("api/[controller]")]
    [ApiController]
    public class SetController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // Implement your API logic here
            return new string[] { "Set 1", "Set 2" };
        }
    }

}
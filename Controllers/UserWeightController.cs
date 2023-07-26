
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Entities;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("[controller]")]
    public class UserWeightController : Controller
    {
        private UserWeightService _userWeightService;

        public UserWeightController(UserWeightService userWeightService)
        {
            _userWeightService = userWeightService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserWeightModel>> GetUserWeight(int id)
        {
            var userWeight = await _userWeightService.GetUserWeight(id);

            if (userWeight == null)
            {
                return NotFound();
            }

            return userWeight;
        }

        [HttpGet("all/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserWeightModel>>> GetAllUserWeights(int id)
        {
            var userWeights = await _userWeightService.GetAllUserWeights(id);

            if (userWeights == null)
            {
                return NotFound();
            }

            return userWeights.ToList();
        }

        [HttpGet("latestWeight/{id}")]
        //[Authorize]
        public async Task<IActionResult> GetLatestWeightAsync(int id)
        {
            var latestWeight = await _userWeightService.GetLatestWeightLogAsync(id);

            if (latestWeight == null)
            {
                // Handle the case where there is no weight log for the user. 
                // You may want to return NotFound or another suitable status code.
                return NotFound("No weight log found for this user.");
            }

            return Ok(latestWeight);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUserWeight(int id, UserWeightModel userWeight)
        {
            bool result = await _userWeightService.UpdateUserWeight(id, userWeight);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostUserWeight([FromBody] UserWeightModel userWeight)
        {
            var newUserWeight = await _userWeightService.CreateUserWeight(userWeight);

            return CreatedAtAction(nameof(GetUserWeight), new { id = newUserWeight.UserId }, newUserWeight);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserWeight(int id)
        {
            var result = await _userWeightService.DeleteUserWeight(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}















//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using userService.Entities;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace userService.Controllers
//{
//    [Route("[controller]")]
//    public class UserWeightController : Controller
//    {
//        private ApplicationDbContext _context;

//        public UserWeightController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/UserWeight/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<UserWeightModel>> GetUserWeight(int id)
//        {
//            // Search for UserWeightModel where the UserId equals id, and order id number descending
//            var userWeight = await _context.UserWeightsLogs
//                .Where(uw => uw.UserId == id)
//                .OrderByDescending(uw => uw.Id)
//                .FirstOrDefaultAsync();

//            if (userWeight == null)
//            {
//                return NotFound();
//            }

//            return userWeight;
//        }

//        // Get All UserWeights by ID
//        [HttpGet("all/{id}")]
//        public async Task<ActionResult<IEnumerable<UserWeightModel>>> GetAllUserWeights(int id)
//        {
//            // Search for UserWeightModel where the UserId equals id, and order by Date descending
//            var userWeights = await _context.UserWeightsLogs
//                .Where(uw => uw.UserId == id)
//                .OrderByDescending(uw => uw.Id)
//                .ToListAsync();

//            if (userWeights == null)
//            {
//                return NotFound();
//            }

//            return userWeights;
//        }

//        // PUT: api/UserWeight/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutUserWeight(int id, UserWeightModel userWeight)
//        {
//            if (id != userWeight.UserId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(userWeight).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!UserWeightExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/UserWeight
//        [HttpPost]
//        public async Task<IActionResult> PostUserWeight([FromBody]UserWeightModel userWeight)
//        {
//            _context.UserWeightsLogs.Add(userWeight);
//            await _context.SaveChangesAsync();

//            CreatedAtAction(nameof(GetUserWeight), new { id = userWeight.UserId }, userWeight);

//            return StatusCode(201);
//        }

//        // DELETE: api/UserWeight/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUserWeight(int id)
//        {
//            var userWeight = await _context.UserWeightsLogs.FindAsync(id);
//            if (userWeight == null)
//            {
//                return NotFound();
//            }

//            _context.UserWeightsLogs.Remove(userWeight);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool UserWeightExists(int id)
//        {
//            return _context.UserWeightsLogs.Any(e => e.UserId == id);
//        }


//    }
//}


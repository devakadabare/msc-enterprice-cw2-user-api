using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserApi.DTO;
using UserApi.Entities;
using UserApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApi.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly UserWeightService _userWeightService;

        public UserController(UserService userService, UserWeightService userWeightService)
        {
            _userService = userService;
            _userWeightService = userWeightService;
        }

        // Action to create a new user
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserRegistrationDTO user)
        {
            var createdUser = await _userService.CreateAsync(user);

            //create a new weight log
            var weightLogResult = await _userWeightService.CreateWeightLogAsync(
                new WeightLogCreationDTO
                {
                    UserId = createdUser.Id,
                    Weight = createdUser.Weight,
                    Date = DateTime.Now
                }
            );

            return Ok(weightLogResult);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel user)
        {
            UserModel foundUser = _userService.Login(user);
            if (foundUser == null)
            {
                return NotFound("User not found");
            }

            string token = _userService.GenerateJwtToken(foundUser);

            return Ok(new { user = foundUser, token = token });
        }

        // Action to get all users
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}

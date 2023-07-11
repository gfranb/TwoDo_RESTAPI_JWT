using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using TwoDo.Data;
using TwoDo.Models;

namespace TwoDo.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly TwoDoDbContext _db;
        public UserController(TwoDoDbContext db) {
            _db = db;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<User> usersquery = await _db.Users.ToListAsync();
                return Ok(usersquery);

            }catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetByIdAuthenticated()
        {
            var stringId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = 0;

            if (stringId != null)
            {
                id = Int32.Parse(stringId);
            }

            try
            {
                User? usersquery = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if(usersquery != null)
                {
                    return Ok(usersquery);
                }
                else
                {
                    return NotFound("User not found");
                }
                

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("admin/{id}")]
        public async Task<IActionResult> GetByIdWithoutAuthentication(int id)
        {

            try
            {
                User? usersquery = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (usersquery != null)
                {
                    return Ok(usersquery);
                }
                else
                {
                    return NotFound("User not found");
                }


            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User newUser)
        {

            try
            {
                if (newUser != null && _db.Users.FirstOrDefault(user => user.Email == newUser.Email) == null)
                {
                    string passwordHash
                        = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);
                    newUser.PasswordHash = passwordHash;
                    _db.Users.Add(newUser);
                    await _db.SaveChangesAsync();
                    return Created("User Created",newUser);
                }
                else
                {
                    return BadRequest("New User is Null or Already email exist");
                }

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var stringId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = 0;

            if (stringId != null)
            {
                id = Int32.Parse(stringId);
            }

            try
            {
                User? user = _db.Users.FirstOrDefault(u => u.UserId == id);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    await _db.SaveChangesAsync();
                    return Ok();
                }

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NotFound("User not found");
        }

        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteWithoutAuth(int id)
        {
            try
            {

                User? user = _db.Users.FirstOrDefault(u => u.UserId == id);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    await _db.SaveChangesAsync();
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NotFound("User not found");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Edit(User editedUser)
        {
            var stringId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = 0;

            if (stringId != null)
            {
                id = Int32.Parse(stringId);
            }

            try
            {
                User? _user = await _db.Users.FirstOrDefaultAsync(user => user.UserId == id);

                if ( _user != null)
                {
                    _user.Name = editedUser.Name;
                    _user.Surname = editedUser.Surname;
                    _user.Email = editedUser.Email;
                    _user.PasswordHash = editedUser.PasswordHash;

                    _db.Users.Update(_user);
                    await _db.SaveChangesAsync();
                    return Ok(_user);

                }
                else
                {
                    return NotFound("User not Found");
                }

            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("admin/{id}")]
        public async Task<IActionResult> EditWithoutAuth(int id, User editedUser)
        {

            try
            {
                User? _user = await _db.Users.FirstOrDefaultAsync(user => user.UserId == id);

                if (_user != null)
                {
                    _user.Name = editedUser.Name;
                    _user.Surname = editedUser.Surname;
                    _user.Email = editedUser.Email;
                    _user.PasswordHash = editedUser.PasswordHash;

                    _db.Users.Update(_user);
                    await _db.SaveChangesAsync();
                    return Ok(_user);

                }
                else
                {
                    return NotFound("User not Found");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }

}

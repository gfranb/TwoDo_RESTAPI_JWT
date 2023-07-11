using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TwoDo.Data;
using TwoDo.Models;

namespace TwoDo.Controllers
{
    [Route("api/asignments")]
    [ApiController]
    public class AssignmentController : Controller
    {
        private readonly TwoDoDbContext _db;
        public AssignmentController(TwoDoDbContext db) {

           _db = db;

        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetWithoutAuthorization() {

            List<Assignment>? list = await _db.Assignments.ToListAsync();
            if (list.Count == 0)
            {
                return NotFound("No hay tareas");
            }
            else
            {
                return Ok(list);
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var stringId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = 0;

            if (stringId != null)
            {
                userId = Int32.Parse(stringId);
            }

            List<Assignment>? list = await _db.Assignments.Where(a => a.UserId == userId).ToListAsync();
            if (list.Count == 0)
            {
                return NotFound("No hay tareas");
            }
            else
            {
                return Ok(list);
            }

        }

        [HttpGet("admin/{id}")]
        public async Task<IActionResult> GetByIdWithoutAuthentication(int id)
        {

            Assignment? assignment = await _db.Assignments.FirstOrDefaultAsync(a => a.Id == id);
            if (assignment == null)
            {
                return NotFound("");
            }
            else
            {
                var response = new
                {
                    assignment.Id,
                    assignment.Title,
                    assignment.Description,
                    assignment.CreationDate,
                    assignment.DueDate,
                    assignment.IsDone,
                    assignment.CompleteDate
                };

                return Ok(response);
            }

        }

        [HttpPost(Name = "CreateAssignmentToUser")]
        [Authorize]
        public async Task<IActionResult> Post(Assignment request)
        {

            var stringId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = 0;

            if (stringId != null) {
                userId = Int32.Parse(stringId);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("The task is null");
            }

            try
            {
                var User = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (User == null)
                {
                    return NotFound("User not Found");
                }

                if(User.Assignments == null)
                {
                    User.Assignments = new List<Assignment>();
                    
                }

                User.Assignments.Add(request);
                await _db.Assignments.AddAsync(request);
                _db.Users.Update(User);
                await _db.SaveChangesAsync();
                return Created("",request);

            }catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("admin/{id}")]
        public async Task<IActionResult> PostWithoutAuthorization(Assignment request, int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("The task is null");
            }

            try
            {
                var User = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (User == null)
                {
                    return NotFound("User not Found");
                }

                if (User.Assignments == null)
                {
                    User.Assignments = new List<Assignment>();

                }

                User.Assignments.Add(request);
                await _db.Assignments.AddAsync(request);
                _db.Users.Update(User);
                await _db.SaveChangesAsync();
                return Created("", request);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteWithoutAuthentication(int id)
        {
            Assignment? query = await _db.Assignments.FirstOrDefaultAsync(a => a.Id == id);

            if(query == null)
            {
                return NotFound("Assignment not foud");
            }

            _db.Assignments.Remove(query);
            await _db.SaveChangesAsync();
            return Ok("Assignment Deleted");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var stringId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = 0;

            if (stringId != null)
            {
                userId = Int32.Parse(stringId);
            }

            Assignment? assignment = await _db.Assignments.FirstOrDefaultAsync(a => a.UserId == userId && a.Id == id);

            if (assignment == null)
            {
                return NotFound("Assignment not foud");
            }

            _db.Assignments.Remove(assignment);
            await _db.SaveChangesAsync();
            return Ok("Assignment Deleted");
        }

        [HttpPut("admin/{id}")]
        public async Task<IActionResult> EditWithoutAutenthication(int id, Assignment assignment)
        {

            try
            {
                Assignment? _assignment = await _db.Assignments.FirstOrDefaultAsync(a => a.Id == id);

                if (await _db.Users.FirstOrDefaultAsync(u => u.UserId == assignment.UserId) == null)
                {
                    return BadRequest("New User to assign not found");
                }

                if (_assignment != null)
                {
                    _assignment.Title = assignment.Title;
                    _assignment.Description = assignment.Description;
                    _assignment.UserId = assignment.UserId;

                    _db.Assignments.Update(_assignment);
                    await _db.SaveChangesAsync();
                    return Ok(_assignment);

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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Edit(Assignment assignment,int id)
        {
            var stringId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = 0;

            if (stringId != null)
            {
                userId = Int32.Parse(stringId);
            }

            try
            {
                Assignment? _assignment = await _db.Assignments.FirstOrDefaultAsync(a => a.UserId == userId && a.Id == id);

                if (await _db.Users.FirstOrDefaultAsync(u => u.UserId == assignment.UserId) == null)
                {
                    return BadRequest("New User to assign not found");
                }

                if (_assignment != null)
                {
                    _assignment.Title = assignment.Title;
                    _assignment.Description = assignment.Description;
                    _assignment.UserId = assignment.UserId;

                    _db.Assignments.Update(_assignment);
                    await _db.SaveChangesAsync();
                    return Ok(_assignment);

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

using csharp_crud_api.Data;
using csharp_crud_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace csharp_crud_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _userContext;

        public UsersController(UserContext userContext)
        {
            _userContext = userContext;
        }

        // GET : api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userContext.Users.ToListAsync();
        }

        // GET : api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userContext.Users.FindAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2001API.Models;

namespace COMP2001API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AsimnusersController : ControllerBase
    {
        private readonly COMP2001_EJacksonContext _context;

        public AsimnusersController(COMP2001_EJacksonContext context)
        {
            _context = context;
        }

        // GET: api/Asimnusers
        [HttpGet]
        public async Task<IActionResult> GetAsimnusers(Asimnuser asimnuser)
        {
            bool valid = getValidation(asimnuser);

            if (valid)
            {
                return Ok(asimnuser);
            }
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsimnuser(int id, Asimnuser asimnuser)
        {
            if (id != asimnuser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(asimnuser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsimnuserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(asimnuser);
        }

        // POST: api/Asimnusers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostAsimnuser(Asimnuser asimnuser) 
        {
          //  _context.Asimnusers.Add(asimnuser);
          //  await _context.SaveChangesAsync();

            string responseString;
            register(asimnuser, out responseString);

            if (responseString != null)
            {
                return Ok(responseString);
            }
            else
            return BadRequest();
        }

        // DELETE: api/Asimnusers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsimnuser(int id)
        {
            var asimnuser = await _context.Asimnusers.FindAsync(id);
            if (asimnuser == null)
            {
                return NotFound();
            }

            _context.Asimnusers.Remove(asimnuser);
            await _context.SaveChangesAsync();

            return Ok(asimnuser);
        }

        private bool AsimnuserExists(int id)
        {
            return _context.Asimnusers.Any(e => e.UserId == id);
        }

        private bool getValidation(Asimnuser user)
        {
            bool valStatus = _context.Validate(user);

            return valStatus;
        }

        private void register(Asimnuser user, out String responseString)
        {
            string outputResponse;
            _context.Register(user, out outputResponse);

            responseString = outputResponse;
        }
    }
}

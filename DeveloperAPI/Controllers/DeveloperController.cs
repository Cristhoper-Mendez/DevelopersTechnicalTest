using DeveloperAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly DeveloperEvaluationBKContext db;

        public DeveloperController()
        {
            db = new DeveloperEvaluationBKContext();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Developer developer)
        {
            try
            {
                developer.CreatedDate = DateTime.Today;
                developer.Enabled = true;

                Request.Headers.TryGetValue("SecurityId", out var securityId);
                Request.Headers.TryGetValue("SecurityKey", out var securityKey);

                if (String.IsNullOrEmpty(securityId) || String.IsNullOrEmpty(securityKey))
                {
                    throw new Exception("No autorizado");
                }

                var security = await db.Securities.FirstOrDefaultAsync(s => s.SecurityId == int.Parse(securityId));

                if (security.SecurityId != int.Parse(securityId) || security.SecurityKey != securityKey)
                {
                    throw new Exception("No autorizado");
                }

                db.Developers.Add(developer);
                await db.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                IEnumerable<Developer> developers;

                Request.Headers.TryGetValue("SecurityId", out var securityId);
                Request.Headers.TryGetValue("SecurityKey", out var securityKey);

                if (String.IsNullOrEmpty(securityId) || String.IsNullOrEmpty(securityKey))
                {
                    throw new Exception("No autorizado");
                }

                var security = await db.Securities.FirstOrDefaultAsync(s => s.SecurityId == int.Parse(securityId));

                if (security.SecurityId != int.Parse(securityId) || security.SecurityKey != securityKey)
                {
                    throw new Exception("No autorizado");
                }

                developers = db.Developers.FromSqlRaw("EXEC SP_GetDevelopers");

                return Ok(developers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                Developer developer = new Developer();

                Request.Headers.TryGetValue("SecurityId", out var securityId);
                Request.Headers.TryGetValue("SecurityKey", out var securityKey);

                if (String.IsNullOrEmpty(securityId) || String.IsNullOrEmpty(securityKey))
                {
                    throw new Exception("No autorizado");
                }

                var security = db.Securities.FirstOrDefault(s => s.SecurityId == int.Parse(securityId));

                if (security.SecurityId != int.Parse(securityId) || security.SecurityKey != securityKey)
                {
                    throw new Exception("No autorizado");
                }

                developer = db.Developers.FirstOrDefault(d => d.DeveloperId == id);

                return Ok(developer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Developer developer)
        {
            try
            {
                if (id != developer.DeveloperId) throw new Exception("Los id's no coinciden");

                Request.Headers.TryGetValue("SecurityId", out var securityId);
                Request.Headers.TryGetValue("SecurityKey", out var securityKey);

                if (String.IsNullOrEmpty(securityId) || String.IsNullOrEmpty(securityKey))
                {
                    throw new Exception("No autorizado");
                }

                var security = db.Securities.FirstOrDefault(s => s.SecurityId == int.Parse(securityId));

                if (security.SecurityId != int.Parse(securityId) || security.SecurityKey != securityKey)
                {
                    throw new Exception("No autorizado");
                }

                var dev = db.Developers.FirstOrDefault(d => d.DeveloperId == id);
                dev.FirstName = developer.FirstName;
                dev.SecondName = developer.SecondName;
                dev.FirstSurname = developer.FirstSurname;
                dev.SecondSurname = developer.SecondSurname;
                dev.Phone = developer.Phone;
                dev.Email = developer.Email;
                dev.ModifiedBy = "";
                dev.ModifiedDate = DateTime.Now;
                dev.Enabled = developer.Enabled;

                db.Update(dev);
                await db.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Request.Headers.TryGetValue("SecurityId", out var securityId);
                Request.Headers.TryGetValue("SecurityKey", out var securityKey);

                if (String.IsNullOrEmpty(securityId) || String.IsNullOrEmpty(securityKey))
                {
                    throw new Exception("No autorizado");
                }

                var security = db.Securities.FirstOrDefault(s => s.SecurityId == int.Parse(securityId));

                if (security.SecurityId != int.Parse(securityId) || security.SecurityKey != securityKey)
                {
                    throw new Exception("No autorizado");
                }

                var dev = db.Developers.FirstOrDefault(d => d.DeveloperId == id);
                dev.Enabled = false;

                db.Update(dev);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

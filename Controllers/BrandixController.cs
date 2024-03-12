using AppCrudeWeb.Models;
using crudApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace crudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandixContext _dbContext;

        public BrandController(BrandixContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Brandix>>> GetBrands()
        {
            if (_dbContext.Brandixs == null)
            {
                return NotFound();
            }

            return await _dbContext.Brandixs.ToListAsync();
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<Brandix>> GetBrand(int id)
        {
            if (_dbContext.Brandixs == null)
            {
                return NotFound();
            }

            var brand = await _dbContext.Brandixs.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }
        [HttpPost]

        public async Task<ActionResult<Brandix>> PostBrand(Brandix brand)
        {
            _dbContext.Brandixs.Add(brand);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.ID }, brand);
        }


        [HttpPut]

        public async Task<ActionResult<Brandix>> PutBrand(int id,Brandix brand)
        {
            if(id != brand.ID)
            {
                return BadRequest();
            }
            _dbContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException) {
            
                if(!BrandixsAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ;
                }
            }
            return Ok();
        }

        private bool BrandixsAvailable(int id)
        {
            return (_dbContext.Brandixs?.Any(x => x.ID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Brandix>> DeleteBrand(int id)
        {
            if (_dbContext.Brandixs == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brandixs.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _dbContext.Brandixs.Remove(brand);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }






        }
    }

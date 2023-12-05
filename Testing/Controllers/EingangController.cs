using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Testing.Data;
using Testing.DTO;
using Testing.Models.Domain;

namespace Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EingangController : ControllerBase
    {
        private readonly DefaultDBContext dbContext;

        public EingangController(DefaultDBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // Get All
        [HttpGet]
        public async Task<IActionResult> GetAllEingang()
        {
            var data = await dbContext.Data.ToListAsync();

            var dataDto = new List<DataDto>();

            foreach (var dataItem in data)
            {
                dataDto.Add(new DataDto()
                {
                    Id = dataItem.Id,
                    Name = dataItem.Name,
                    Surename = dataItem.Surename,
                    Age = dataItem.Age,
                    Email = dataItem.Email,
                    Address = dataItem.Address
                });
            }

            return Ok(dataDto);
        }

        // Get by ID
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var data = await dbContext.Data.FindAsync(id);

            var dataDto = new List<DataDto>();

            if (data != null)
            {
                dataDto.Add(new DataDto()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Surename = data.Surename,
                    Age = data.Age,
                    Email = data.Email,
                    Address = data.Address
                });
            }


            if (data == null)
            {
                return NotFound();
            }

            return Ok(dataDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DataDtoRequest dataDtoRequest)

        {
            var data = new DataDb

            {
                Id = Guid.NewGuid(),
                Name = dataDtoRequest.Name,
                Surename = dataDtoRequest.Surename,
                Age = dataDtoRequest.Age,
                Email = dataDtoRequest.Email,
                Address = dataDtoRequest.Address
            };

            await dbContext.Data.AddAsync(data);
            await dbContext.SaveChangesAsync();

            var dataDto = new DataDto
            {
                Id = data.Id,
                Name = data.Name,
                Surename = data.Surename,
                Age = data.Age,
                Email = data.Email,
                Address = data.Address
            };

            return CreatedAtAction(nameof(GetById), new { id = dataDto.Id}, dataDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] DataDtoRequest dataDtoRequest)
        {
            var data = await dbContext.Data.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            data.Name = dataDtoRequest.Name;
            data.Surename = dataDtoRequest.Surename;
            data.Age = dataDtoRequest.Age;
            data.Email = dataDtoRequest.Email;
            data.Address = dataDtoRequest.Address;

            dbContext.Data.Update(data);
            await dbContext.SaveChangesAsync();

            var dataDto = new DataDto
            {
                Id = data.Id,
                Name = data.Name,
                Surename = data.Surename,
                Age = data.Age,
                Email = data.Email,
                Address = data.Address
            };

            return CreatedAtAction(nameof(GetById), new { id = dataDto.Id }, dataDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public  async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var data = await dbContext.Data.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            
            dbContext.Data.Remove(data);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

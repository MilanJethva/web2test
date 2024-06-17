using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using web2test.Data;
using web2test.Models.Domain;
using web2test.Models.DTO;
using web2test.Models.Repositories;

namespace web2test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly TestContext dbcontext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(TestContext dbcontext, IRegionRepository regionRepository)
        {
            this.dbcontext = dbcontext;
            this.regionRepository = regionRepository;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Coode = regionDomain.Coode,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });

            }

            return Ok(regionsDomain);
        }


        //// Get All Regions
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //       // var regionsDomain = await regionRepository.GetAllAsync();
        //       //var regionsDomain = async Task<IActionResult> GetAllAsync()
        //       var regionDomain=dbcontext.Regions

        //        var regionsDto = regionDomain.Select(region => new RegionDto
        //        {
        //            Id = region.Id,
        //            Coode = region.Coode,
        //            Name = region.Name,
        //            RegionImageUrl = region.RegionImageUrl
        //        }).ToList();

        //        return Ok(regionsDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "An error occurred while getting all regions.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while  in get allprocessing your request.");
        //    }
        //}

        // Get Single Region (Get Region By ID)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            
                var regionDomain = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
                if (regionDomain == null)
                {
                    return NotFound();
                }

                var regionDto = new RegionDto
                {
                    Id = regionDomain.Id,
                    Coode = regionDomain.Coode,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                };

                return Ok(regionDto);
            
            
        }

        // Create New Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
                var regionDomainModel = new Region
                {
                    Coode = addRegionRequestDto.Coode,
                    Name = addRegionRequestDto.Name,
                    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
                };

                await dbcontext.Regions.AddAsync(regionDomainModel);
                await dbcontext.SaveChangesAsync();

                var regionDto = new RegionDto
                {
                    Id = regionDomainModel.Id,
                    Coode = regionDomainModel.Coode,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl,
                };

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            
        }

        // Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
                var regionDomainModel = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                regionDomainModel.Coode = updateRegionRequestDto.Coode;
                regionDomainModel.Name = updateRegionRequestDto.Name;
                regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

                await dbcontext.SaveChangesAsync();

                var regionDto = new RegionDto
                {
                    Id = regionDomainModel.Id,
                    Coode = regionDomainModel.Coode,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl,
                };

                return Ok(regionDto);
           
        }

        // Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
                var regionDomainModel = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                dbcontext.Regions.Remove(regionDomainModel);
                await dbcontext.SaveChangesAsync();

                var regionDto = new RegionDto
                {
                    Id = regionDomainModel.Id,
                    Coode = regionDomainModel.Coode,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl,
                };

                return Ok(regionDto);
           
        }
    }
}
  
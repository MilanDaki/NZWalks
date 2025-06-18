using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper)  
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // Get all regions 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from db - domain model
            var regionDomain = await regionRepository.GetAllAsync();

            // Return the data - DTO model
            return Ok(mapper.Map<List<RegionDto>>(regionDomain));
        }

        // Get region by id 
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            // var region = dbContext.Regions.Find(id);
            // Get Region Domain Model from db
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id); 

            if (regionDomain == null)
            {
                return NotFound();
            }       
            //return Dto back to client 
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        // Post to create new region
        // 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain model to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            // Map Domain model to DTO model
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { Id = regionDomainModel.id }, regionDomainModel);
        }

        // Upadate region 
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {        
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            
            // check if region exists in db
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            } 
           
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // Delete region by id 
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Get region from db
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Return Ok
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
     
    }

}


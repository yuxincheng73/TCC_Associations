using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using TCCAssociation.Associations;
using TCCAssociation.Associations.Dto;

namespace TCCAssociation.Controllers
{
    [ApiController]
    [Route("api")]
    public class AssociationsController : TCCAssociationControllerBase
    {
        private readonly IAssociationsService _associationsService;

        public AssociationsController(IAssociationsService associationsService)
        {
            _associationsService = associationsService;
        }

        [HttpGet]
        [Route("associations")]
        public async Task<IActionResult> GetAssociations()
        {
            var associations = await _associationsService.GetAssociations();
            if(associations == null)
            {
                return NotFound();
            }
            return Ok(associations);
        }

        [HttpGet]
        [Route("associations/{id}")]
        public async Task<IActionResult> GetAssociation(int id)
        {
            var association = await _associationsService.GetAssociation(id);
            if(association == null)
            {
                return NotFound();
            }
            return Ok(association);
        }

        [HttpPost]
        [Route("associations")]
        public async Task<IActionResult> CreateAssociation(AssociationDto input)
        {
            if(input == null)
            {
                return BadRequest();
            }
            var association = await _associationsService.CreateAssociation(input);
            if(association == -1)
            {
                return BadRequest();
            }
            return Ok(association);
        }

        [HttpPut]
        [Route("associations/{id}")]
        public async Task<IActionResult> UpdateAssociation(int id, AssociationDto input)
        {
            if(input == null)
            {
                return BadRequest();
            }
            var association = await _associationsService.UpdateAssociation(id, input);
            if(association == null)
            {
                return BadRequest();
            }
            return Ok(association);
        }

        [HttpDelete]
        [Route("associations")]
        public async Task<IActionResult> DeleteAssociation(EntityDto id)
        {
            var association = await _associationsService.DeleteAssociation(id);
            if(association == -1)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}

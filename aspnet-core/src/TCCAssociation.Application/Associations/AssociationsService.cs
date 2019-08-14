using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using MongoDB.Driver;
using TCCAssociation.Associations.Dto;
using TCCAssociation.Models;

namespace TCCAssociation.Associations
{
    public class AssociationsService : TCCAssociationAppServiceBase, IAssociationsService
    {
        private readonly IRepository<Association> _associationRepository;

        public AssociationsService(IRepository<Association> associationRepository)
        {
            _associationRepository = associationRepository;
        }

        public async Task<int> CreateAssociation(AssociationDto input)
        {
            if(input == null)
            {
                return -1;
            }
            var association = ObjectMapper.Map<Association>(input);
            association.Discoverable = false;
            association.SortingOrder = association.Id;
            association.TimeZone = DateTime.UtcNow.ToString();
            association.LaunchDate = association.CreationTime;
            await _associationRepository.InsertAndGetIdAsync(association);
            return association.Id;
        }

        public async Task<int> DeleteAssociation(EntityDto input)
        {
            if(input == null)
            {
                return -1;
            }
            await _associationRepository.DeleteAsync(input.Id);
            return 1;
        }

        public async Task<AssociationDto> GetAssociation(int id)
        {
            var association = await _associationRepository.FirstOrDefaultAsync(c => c.Id == id);
            if(association == null)
            {
                return null;
            }
            var associationToReturn = ObjectMapper.Map<AssociationDto>(association);
            return associationToReturn;
        }

        public async Task<List<AssociationDto>> GetAssociations()
        {
            var associations = await _associationRepository.GetAllListAsync();
            if (associations == null)
            {
                return null;
            }
            foreach(Association association in associations)
            {
                if(association.Name == null)
                {
                    associations.Remove(association);
                }
            }
            var associationsToReturn = ObjectMapper.Map<List<AssociationDto>>(associations);
            return associationsToReturn;
        }

        public async Task<AssociationDto> UpdateAssociation(int id, AssociationDto input)
        {
            if(input == null)
            {
                return null;
            }

            var association = ObjectMapper.Map<Association>(input);
            association.Id = id;
            var associationUpdated = await _associationRepository.UpdateAsync(association);
            var associationToReturn = ObjectMapper.Map<AssociationDto>(associationUpdated);
            return associationToReturn;
        }
    }
}

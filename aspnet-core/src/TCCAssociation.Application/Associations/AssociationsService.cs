using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using TCCAssociation.Associations.Dto;
using TCCAssociation.Models;

namespace TCCAssociation.Associations
{
    public class AssociationsService : TCCAssociationAppServiceBase, IAssociationsService
    {
        private readonly IMongoCollection<Association> _associationRepository;

        public AssociationsService(IAssociationsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _associationRepository = database.GetCollection<Association>(settings.BrandsCollectionName);
        }

        public async Task<string> CreateAssociation(AssociationDto input)
        {
            if(input == null)
            {
                return "Invalid Creation of Association";
            }

            var association = ObjectMapper.Map<Association>(input);
            association.Discoverable = false;
            association.SortingOrder = association.Id;
            association.TimeZone = DateTime.UtcNow.ToString();
            association.LaunchDate = association.CreationTime;
            await _associationRepository.InsertOneAsync(association);
            return association.Id;

        }

        public async Task<string> DeleteAssociation(string id)
        {
            if(id == null)
            {
                return "Deletion Failed";
            }
            await _associationRepository.DeleteOneAsync(c => c.Id == id);
            return "Deletion Succesfull";
        }

        public async Task<AssociationDto> GetAssociation(string id)
        {
            if(id == null)
            {
                return null;
            }
            var association = await _associationRepository.FindAsync(c => c.Id == id);
            var associationToReturn = ObjectMapper.Map<AssociationDto>(association);
            return associationToReturn;
        }

        public async Task<List<AssociationDto>> GetAssociations()
        {
            var associations = await _associationRepository.Find(c => true).ToListAsync();
            var associationsToReturn = new List<AssociationDto>();
            foreach(Association association in associations)
            {
                associationsToReturn.Add(ObjectMapper.Map<AssociationDto>(association));
            }
            if(associationsToReturn == null)
            {
                return null;
            }
            return associationsToReturn;
        }

        public async Task<AssociationDto> UpdateAssociation(AssociationDto input)
        {
            if(input == null)
            {
                return null;
            }

            var association = ObjectMapper.Map<Association>(input);
            var associationToUpdate = Builders<Association>.Update.Set("Id", association.Id)
                .Set("Name", association.Name)
                .Set("Name_TC", association.Name_TC)
                .Set("Name_SC", association.Name_SC)
                .Set("Description", association.Description)
                .Set("Description_TC", association.Description_TC)
                .Set("Description_SC", association.Description_SC)
                .Set("Url", association.Url)
                .Set("Url_TC", association.Url_TC)
                .Set("Url_SC", association.Url_SC)
                .Set("Logo", association.Logo)
                .Set("TimeZone", association.TimeZone);
            var associationUpdated = await _associationRepository.FindOneAndUpdateAsync(Builders<Association>.Filter.Eq(c => c.Id, association.Id), associationToUpdate, new FindOneAndUpdateOptions<Association> { ReturnDocument = ReturnDocument.After });
            var associationToReturn = ObjectMapper.Map<AssociationDto>(associationUpdated);
            return associationToReturn;
        }
    }
}

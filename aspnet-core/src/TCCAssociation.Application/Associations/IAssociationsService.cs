using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCCAssociation.Associations.Dto;

namespace TCCAssociation.Associations
{
    public interface IAssociationsService : IApplicationService
    {
        Task<List<AssociationDto>> GetAssociations();
        Task<AssociationDto> GetAssociation(int id);
        //Task<AssociationDto> GetAssociationbyName(string name, string language_code);
        Task<int> CreateAssociation(AssociationDto input);
        Task<AssociationDto> UpdateAssociation(int id, AssociationDto input);
        Task<int> DeleteAssociation(EntityDto id);
    }
}

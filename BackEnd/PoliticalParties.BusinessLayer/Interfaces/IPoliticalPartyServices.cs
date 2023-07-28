using PoliticalParties.BusinessLayer.ViewModels;
using PoliticalParties.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliticalParties.BusinessLayer.Interfaces
{
    public interface IPoliticalPartyServices
    {
        Task<PoliticalParty> Create(PoliticalParty politicalParty);
        Task<PoliticalParty> GetById(long politicalPartyId);
        Task<IEnumerable<PoliticalParty>> GetByPartyName(string politicalPartyName);
        Task<IEnumerable<PoliticalParty>> GetByFounderName(string politicalPartyFounderName);
        Task<PoliticalParty> Update(RegisterPoliticalPartyViewModel model);
        Task<PoliticalParty> Delete(RegisterPoliticalPartyViewModel model);
        Task<IEnumerable<PoliticalParty>> GetAll();
    }
}

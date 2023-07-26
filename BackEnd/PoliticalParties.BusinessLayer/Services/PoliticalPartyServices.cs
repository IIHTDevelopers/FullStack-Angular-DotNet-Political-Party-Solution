using PoliticalParties.BusinessLayer.Interfaces;
using PoliticalParties.BusinessLayer.Services.Repository;
using PoliticalParties.BusinessLayer.ViewModels;
using PoliticalParties.DataLayer;
using PoliticalParties.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliticalParties.BusinessLayer.Services
{
    public class PoliticalPartyServices : IPoliticalPartyServices
    {
        private readonly IPoliticalPartyRepository _politicalPartyRepository;
        private readonly PoliticalPartiesDbContext _politicalPartiesDbContext;
        public PoliticalPartyServices(IPoliticalPartyRepository politicalPartyRepository,PoliticalPartiesDbContext politicalPartiesDbContext)
        {
            _politicalPartyRepository = politicalPartyRepository;
            _politicalPartiesDbContext = politicalPartiesDbContext;
        }

        public async Task<PoliticalParty> GetByPartyName(string politicalPartyName)
        {
            return await _politicalPartyRepository.GetByPartyName(politicalPartyName); 
        }

        public async Task<PoliticalParty> GetByFounderName(string politicalPartyFounderName)
        {
            return await _politicalPartyRepository.GetByFounderName(politicalPartyFounderName);
        }

        public async Task<PoliticalParty> GetById(long politicalPartyId)
        {
            return await _politicalPartyRepository.GetById(politicalPartyId);
        }

        public async Task<IEnumerable<PoliticalParty>> GetAll()
        {
            return await _politicalPartyRepository.GetAll();
        }

        public async Task<PoliticalParty> Create(PoliticalParty politicalParty)
        {
            return await _politicalPartyRepository.Create(politicalParty);
        }

        public async Task<PoliticalParty> Update(RegisterPoliticalPartyViewModel model)
        {
            return await _politicalPartyRepository.Update(model);
        }
        public async Task<PoliticalParty> Delete(RegisterPoliticalPartyViewModel model)
        {
            return await _politicalPartyRepository.Delete(model);
        }
    }
}

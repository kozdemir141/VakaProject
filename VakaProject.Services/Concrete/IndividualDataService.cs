using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VakaProject.Data.Abstract;
using VakaProject.Domain.Concrete;
using VakaProject.Domain.Dtos;
using VakaProject.Services.Abstract;

namespace VakaProject.Services.Concrete
{
    public class IndividualDataService : ManagerBase, IIndividualDataService
    {
        public IndividualDataService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public async Task CreateAsync(IndividualData individualData)
        {
            await UnitOfWork.IndividualData.AddAsync(individualData);
            await UnitOfWork.SaveAsync();
        }

        public async Task<IList<IndividualWithProfileDto>> GetWithProfilesAsync(CancellationToken cancellationToken = default)
        {
            var allIndividuals = await UnitOfWork.IndividualData.GetAllAsync();
            var allProfiles    = await UnitOfWork.DataProfile.GetAllAsync();

            await AssignProfilesAsync(allIndividuals, allProfiles);

            allIndividuals = await UnitOfWork.IndividualData.GetAllAsync();
            allProfiles    = await UnitOfWork.DataProfile.GetAllAsync();

            var result = (from ind in allIndividuals
                    join prof in allProfiles on ind.Id equals prof.IndividualDataId
                    select new IndividualWithProfileDto
                    {
                        Id             = ind.Id,
                        FirstName      = ind.FirstName,
                        MiddleName     = ind.MiddleName,
                        LastName       = ind.LastName,
                        BirthPlace     = ind.BirthPlace,
                        BirthDate      = ind.BirthDate,
                        Nationality    = ind.Nationality,
                        IdentityNumber = ind.IdentityNumber,   
                        ProfileId      = prof.ProfileId   
                    })
                .ToList();

            return result;
        }

        private async Task AssignProfilesAsync(
            IList<IndividualData> allIndividuals,
            IList<DataProfile>    allProfiles)
        {
            var unprofiled = allIndividuals
                .Where(i => !allProfiles.Any(p => p.IndividualDataId == i.Id))
                .ToList();

            var profileGroups = allProfiles
                .GroupBy(p => p.ProfileId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(dp => allIndividuals.Single(i => i.Id == dp.IndividualDataId)).ToList()
                );

            foreach (var person in unprofiled.ToList())
            {
                var match = TryMatchToExisting(person, profileGroups);
                if (match.HasValue)
                {
                    await UnitOfWork.DataProfile.AddAsync(new DataProfile
                    {
                        ProfileId        = match.Value,
                        IndividualDataId = person.Id
                    });
                    unprofiled.Remove(person);
                }
            }

            if (unprofiled.Any())
            {
                var nextProfileId = allProfiles.Any()
                    ? allProfiles.Max(p => p.ProfileId) + 1
                    : 1;

                var clusters = ClusterUnprofiled(unprofiled);
                foreach (var cluster in clusters)
                {
                    foreach (var member in cluster)
                    {
                        await UnitOfWork.DataProfile.AddAsync(new DataProfile
                        {
                            ProfileId        = nextProfileId,
                            IndividualDataId = member.Id
                        });
                    }
                    nextProfileId++;
                }
            }

            await UnitOfWork.SaveAsync();
        }

        private int? TryMatchToExisting(
            IndividualData person,
            IDictionary<int, List<IndividualData>> existingGroups)
        {
            foreach (var kv in existingGroups)
            {
                var profileId = kv.Key;
                var members   = kv.Value;

                bool isSimilar = members.Any(m =>
                    string.Equals(m.FirstName, person.FirstName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(m.LastName,  person.LastName,  StringComparison.OrdinalIgnoreCase) &&
                    m.BirthDate.Date == person.BirthDate.Date);

                if (isSimilar)
                    return profileId;
            }

            return null;
        }
        
        private List<List<IndividualData>> ClusterUnprofiled(List<IndividualData> unprofiled)
        {
            return unprofiled
                .Select(i => new List<IndividualData> { i })
                .ToList();
        }
    }
}

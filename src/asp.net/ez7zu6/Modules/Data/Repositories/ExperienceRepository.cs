using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Database;
using ez7zu6.Data.Models.Experience;

namespace ez7zu6.Data.Repositories
{
    public class ExperienceRepository : BaseRepository
    {
        public ExperienceRepository(IAppEnvironment appEnvironment) : base(appEnvironment) { }

        public async Task<int> AddExperience(ExperienceUpdaeDataModel dataModel)
        {
            using (var db = GetConnection())
            {
                string sql = @"
                insert into dbo.Experiences(UserId, Notes, InputDateTime) 
                values (@UserId, @Notes, @InputDateTime)
                select cast(scope_identity() as int)";

                var id = await db.QueryAsync<int>(sql, new { dataModel.UserId, dataModel.Notes, dataModel.InputDateTime, });
                return id.Single();
            }
        }

        // TODO: add a sort order field like we had in the weather app
        public async Task<List<ExperienceQueryDataModel>> GetExperiencesByUserId(Guid userId)
        {
            var take = 4; // TODO: get from configuration / settings
            using (var db = GetConnection())
            {
                const string query = @"
select top(@Take) ExperienceId, Notes, InputDateTime
from dbo.Experiences (nolock) 
where UserId = @UserId 
order by InputDateTime desc";

                var experiences = await db.QueryAsync<ExperienceQueryDataModel>(query, new { UserId = userId, Take = take, });
                return experiences.ToList();
            }
        }
    }
}

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

        public async Task AddExperience(ExperienceUpdaeDataModel dataModel)
        {
            using (var db = GetConnection())
            {
                string sql = @"
                insert into dbo.Experiences(ExperienceId, UserId, Notes, InputDateTime) 
                values (@ExperienceId, @UserId, @Notes, @InputDateTime)";

                await db.ExecuteAsync(sql, new { dataModel.ExperienceId, dataModel.UserId, dataModel.Notes, dataModel.InputDateTime, });
            }
        }

        // TODO: add a sort order on the fly, then work off of that ... we will need to track the "previous" sort number, though, like in the weather app
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

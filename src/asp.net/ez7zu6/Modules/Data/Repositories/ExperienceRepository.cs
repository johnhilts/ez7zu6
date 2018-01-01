﻿using System;
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

        public async Task AddExperience(ExperienceUpdateDataModel dataModel)
        {
            using (var db = GetConnection())
            {
                string sql = @"
                insert into dbo.Experiences(ExperienceId, UserId, Notes, InputDateTime) 
                values (@ExperienceId, @UserId, @Notes, @InputDateTime)";

                await db.ExecuteAsync(sql, new { dataModel.ExperienceId, dataModel.UserId, dataModel.Notes, dataModel.InputDateTime, });
            }
        }

        public async Task<List<ExperienceQueryDataModel>> GetExperiencesByUserId(Guid userId, int numberOfExperiences, int? previousSortOrder)
        {
            using (var db = GetConnection())
            {
                const string query = @"
with UserExperiences as 
(
select ROW_NUMBER() over (order by e.InputDateTime desc, e.ExperienceId) as RowId, e.ExperienceId
from dbo.Experiences e (nolock) 
where e.UserId = @UserId
)
select e.ExperienceId, Notes, InputDateTime
from dbo.Experiences e (nolock) 
	inner join UserExperiences ue on e.ExperienceId = ue.ExperienceId
where ue.RowId between @StartIndex and @EndIndex";

                var startIndex = previousSortOrder.GetValueOrDefault() + 1;
                var endIndex = startIndex + numberOfExperiences - 1;

                var experiences = await db.QueryAsync<ExperienceQueryDataModel>(query, new { UserId = userId, StartIndex = startIndex, EndIndex = endIndex, });
                return experiences.ToList();
            }
        }
    }
}

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

        public async Task<ExperienceQueryDataResultModel> GetExperiencesByUserId(Guid userId, int startIndex, int endIndex)
        {
            using (var db = GetConnection())
            {
                const string query = @"
declare @experiences table(RowId int, ExperienceId uniqueidentifier);
with UserExperiences as 
(
select ROW_NUMBER() over (order by e.InputDateTime desc, e.ExperienceId) as RowId, e.ExperienceId
from dbo.Experiences e (nolock) 
where e.UserId = @UserId
)
insert into @experiences
select * from UserExperiences
select e.ExperienceId, Notes, InputDateTime
from dbo.Experiences e (nolock) 
	inner join @experiences ue on e.ExperienceId = ue.ExperienceId
where ue.RowId between @StartIndex and @EndIndex

declare @totalRowLength int = (select count(*) from @experiences)
select @totalRowLength TotalRowLength
";

                var experienceInfo = await db.QueryMultipleAsync(query, new { UserId = userId, StartIndex = startIndex, EndIndex = endIndex, });
                var experiences = experienceInfo.Read<ExperienceDataModel>();
                var totalRowCount = experienceInfo.Read<int>().Single();
                return new ExperienceQueryDataResultModel { Experiences = experiences.ToList(), TotalRowCount = totalRowCount, };
            }
        }
    }
}

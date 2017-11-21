using System.Linq;
using System.Threading.Tasks;
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

    }
}

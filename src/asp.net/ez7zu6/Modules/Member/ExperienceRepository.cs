using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Core;
using Infrastructure.Database;

namespace Member
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

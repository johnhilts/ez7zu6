using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ez7zu6.Core;
using ez7zu6.Core.Util;
using ez7zu6.Core.Extensions;
using ez7zu6.Data.Models.Account;
using ez7zu6.Data.Models.Experience;
using ez7zu6.Data.Repositories;
using ez7zu6.Member.Models;

namespace ez7zu6.Member.Services
{
    public class MemberService
    {
        private readonly IAppEnvironment _appEnvironment;

        public MemberService(IAppEnvironment appEnvironment) => _appEnvironment = appEnvironment;

        public async Task<UserInfoModel> GetUserInfoByUsernameAndPassword(string username, string password)
        {
            var queryModel = await (new AccountRepository(_appEnvironment)).GetUserInfoByUsernameAndPassword(username, password);
            if (queryModel.NoMatch || IsInvalidPassword(queryModel, password))
            {
                return new UserInfoModel { UserId = null, Username = null, CanAuthenticate = false, };
            }
            else
            {
                return new UserInfoModel { UserId = queryModel.UserId, Username = queryModel.Username, CanAuthenticate = true, };
            }
        }

        private bool IsInvalidPassword(AccountQueryModel queryModel, string inputPassword)
        {
            return queryModel.UserPassword.GetHexStringFromBytes().Equals(GetPasswordHash(queryModel.UserId, queryModel.Username, inputPassword));
        }

        public async Task<List<ExperienceQueryModel>> GetExperiences(Guid userId)
        {
            var experiencesData = await (new ExperienceRepository(_appEnvironment)).GetExperiencesByUserId(userId);
            var experiences = experiencesData
                .Select(data => new ExperienceQueryModel { ExperienceId = data.ExperienceId, Notes = data.Notes, InputDateTime = data.InputDateTime });
            return experiences.ToList();
        }

        public async Task<Guid> SaveExperience(ExperienceSaveModel model, Guid userId)
        {
            var experienceId = Guid.NewGuid();
            var dataModel = new ExperienceUpdaeDataModel
            {
                ExperienceId = experienceId,
                UserId = userId,
                Notes = model.Notes,
                InputDateTime = model.InputDateTime,
            };
            await (new ExperienceRepository(_appEnvironment)).AddExperience(dataModel);
            return experienceId;
        }

        public async Task<Guid> Register(RegisterModel model)
        {
            var userId = Guid.NewGuid();
            var password = GetPasswordHash(userId, model.Username, model.Password);
            var dataModel = new AccountCreateModel
            {
                UserId = userId,
                Username = model.Username,
                UserPassword = password,
                IsAnonymous = false,
                OptedIn = DateTime.Today,
            };
            await (new AccountRepository(_appEnvironment)).AddUser(dataModel);
            return userId;
        }

        private string GetPasswordHash(Guid userId, string username, string password)
        {
            string salt = userId.ToString() + username;
            return (new EncryptionHelper().GenerateHash(salt, password)).GetHexStringFromBytes();
        }
    }
}

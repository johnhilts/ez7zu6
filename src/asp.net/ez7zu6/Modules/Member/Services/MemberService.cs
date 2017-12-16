﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ez7zu6.Core;
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
            if (queryModel.NoMatch)
            {
                return new UserInfoModel { UserId = null, Username = null, CanAuthenticate = false, };
            }
            else
            {
                return new UserInfoModel { UserId = queryModel.UserId, Username = queryModel.Username, CanAuthenticate = true, };
            }
        }

        public async Task<List<ExperienceQueryModel>> GetExperiences(Guid userId)
        {
            var experiencesData = await (new ExperienceRepository(_appEnvironment)).GetExperiencesByUserId(userId);
            var experiences = experiencesData
                .Select(data => new ExperienceQueryModel { ExperienceId = data.ExperienceId, Notes = data.Notes, InputDateTime = data.InputDateTime });
            return experiences.ToList();
        }

        public async Task<int> SaveExperience(ExperienceSaveModel model, Guid userId)
        {
            var dataModel = new ExperienceUpdaeDataModel
            {
                UserId = userId,
                Notes = model.Notes,
                InputDateTime = model.InputDateTime,
            };
            return await (new ExperienceRepository(_appEnvironment)).AddExperience(dataModel);
        }
    }
}

namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Domain;
    using Domain.Contracts.Repositories;
    using Domain.Contracts.Tasks;
    using Domain.Specifications;

    using Framework.Validation;
    using Framework.Extensions;

    using SharpArch.Core;

    #endregion

    public class ProfileQueryTasks : IProfileQueryTasks
    {
        #region Fields

        private readonly IProfileRepository profileRepository;

        #endregion

        public ProfileQueryTasks(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public Profile GetProfileById(int profileId)
        {
            return this.profileRepository.FindOne(new ProfileByIdSpecification(profileId));
        }

        public Profile GetProfileByUserName(string userName)
        {
            return this.profileRepository.FindOne(new ProfileByUserNameSpecification(userName));
        }
    }
}

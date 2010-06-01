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

    public class ProfileCommandTasks : IProfileCommandTasks
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProfileRepository profileRepository;
        private readonly ITagRepository tagRepository;
        private readonly IProfileQueryTasks profileQueryTasks;

        public ProfileCommandTasks(
            IProfileRepository profileRepository, 
            ITagRepository tagRepository,
            ICategoryRepository categoryRepository, 
            IProfileQueryTasks profileQueryTasks)
        {
            this.profileRepository = profileRepository;
            this.profileQueryTasks = profileQueryTasks;
            this.tagRepository = tagRepository;
            this.categoryRepository = categoryRepository;
        }

        public void AddAssertion(string userName, int categoryId, string tagName)
        {
            Check.Require(!userName.IsNullOrEmpty(), "userName is required.");
            Check.Require(!tagName.IsNullOrEmpty(), "tagName is required.");
            Check.Require(categoryId > 0, "categoryId must be greater than 0");

            // TODO: Ideally we want a transaction here as we are potentially doing two updates.

            var profile = this.profileQueryTasks.GetProfileByUserName(userName);

            var tag = this.tagRepository.FindOne(new TagByNameSpecification(tagName));

            if (tag == null)
            {
                tag = new Tag
                {
                    Name = tagName
                };

                this.tagRepository.Save(tag);
            }

            // See if there's already an assertion for this tag/category combination
            var existingAssertion = profile.Assertions.FirstOrDefault(
                a => (a.Tag == tag) && (a.Category.Id == categoryId));

            // If not add it. If there is, do nothing further - there's no point returning an error, since the user has what they wanted
            if (existingAssertion == null)
            {
                var category = this.GetCategory(categoryId);

                // TODO: More validation... what if category is null.
                var newAssertion = new Assertion
                {
                    Profile = profile,
                    Category = category,
                    Tag = tag
                };

                profile.Assertions.Add(newAssertion);

                this.profileRepository.Save(profile);
            }
        }

        public void CreateProfile(string userName, string firstName, string lastName)
        {
            Check.Require(!userName.IsNullOrEmpty(), "userName is required.");
            Check.Require(!firstName.IsNullOrEmpty(), "firstName is required.");
            Check.Require(!lastName.IsNullOrEmpty(), "lastName is required.");

            var profile = new Profile
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                CreatedOn = DateTime.Now.Date
            };

            // Should use nhibernate sql exception convertor to catch unique constraint violation and convert 
            // to an appropriate exception type.
            this.profileRepository.Save(profile);
        }

        public void DeleteProfile(string userId)
        {
            Check.Require(!userId.IsNullOrEmpty());

            var profile = this.profileQueryTasks.GetProfileByUserName(userId);

            if (profile != null)
            {
                this.profileRepository.Delete(profile);
            }
        }


        public void RemoveAssertion(Profile profile, int assertionId)
        {
            var assertionToRemove = profile.Assertions.FirstOrDefault(a => a.Id == assertionId);

            if (assertionToRemove != null)
            {
                profile.Assertions.Remove(assertionToRemove);

                this.profileRepository.Save(profile);
            }
        }

        private Category GetCategory(int categoryId)
        {
            Check.Require(categoryId > 0);

            var category = this.categoryRepository.FindOne(new CategoryByIdSpecification(categoryId));

            Check.Ensure(category != null, "The specified category does not exist.");

            return category;
        }

    }
}

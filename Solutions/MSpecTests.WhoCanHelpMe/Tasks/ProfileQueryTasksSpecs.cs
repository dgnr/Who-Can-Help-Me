namespace MSpecTests.WhoCanHelpMe.Tasks
{
    #region Using Directives

    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Repositories;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Domain.Specifications;
    using global::WhoCanHelpMe.Tasks;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;

    #endregion

    public abstract class specification_for_profile_query_tasks : Specification<IProfileQueryTasks, ProfileQueryTasks>
    {
        protected static IProfileRepository the_profile_repository;
        protected static ITagRepository the_tag_repository;
        protected static ICategoryRepository the_category_repository;

        Establish context = () =>
            {
                the_profile_repository = DependencyOf<IProfileRepository>();
                the_tag_repository = DependencyOf<ITagRepository>();
                the_category_repository = DependencyOf<ICategoryRepository>();

                ServiceLocatorHelper.InitialiseServiceLocator();
                ServiceLocatorHelper.AddValidator();
            };
    }

    [Subject(typeof(ProfileQueryTasks))]
    public class when_the_profile_query_tasks_is_asked_to_get_a_profile_by_user_name : specification_for_profile_query_tasks
    {
        static Profile result;
        static string user_name;
        static Profile the_profile;

        Establish context = () =>
            {
                user_name = "user_name";

                the_profile = new Profile();

                the_profile_repository.StubFindOne().Return(the_profile);
            };

        Because of = () => result = subject.GetProfileByUserName(user_name);

        It should_ask_the_repository_for_the_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByUserNameSpecification, Profile>(spec => spec.UserName == user_name);

        It should_return_the_profile_it_retrieved_from_the_repository = () => result.ShouldBeTheSameAs(the_profile);
    }

    [Subject(typeof(ProfileQueryTasks))]
    public class when_the_profile_query_tasks_is_asked_to_get_a_profile_by_user_name_and_there_is_no_matching_profile : specification_for_profile_query_tasks
    {
        static Profile result;
        static string user_name;

        Establish context = () =>
        {
            user_name = "user_name";
            the_profile_repository.StubFindOne().Return(null);
        };

        Because of = () => result = subject.GetProfileByUserName(user_name);

        It should_ask_the_repository_for_the_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByUserNameSpecification, Profile>(spec => spec.UserName == user_name);

        It should_return_null = () => result.ShouldBeNull();
    }

    [Subject(typeof(ProfileQueryTasks))]
    public class when_the_profile_query_tasks_is_asked_to_get_a_profile_by_profile_id : specification_for_profile_query_tasks
    {
        static Profile result;
        static int profile_id;
        static Profile the_profile;

        Establish context = () =>
        {
            profile_id = 1;

            the_profile = new Profile();

            the_profile_repository.StubFindOne().Return(the_profile);
        };

        Because of = () => result = subject.GetProfileById(profile_id);

        It should_ask_the_repository_for_the_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByIdSpecification, Profile>(spec => spec.Id == profile_id);

        It should_return_the_profile_it_retrieved_from_the_repository = () => result.ShouldBeTheSameAs(the_profile);
    }

    [Subject(typeof(ProfileQueryTasks))]
    public class when_the_profile_query_tasks_is_asked_to_get_a_profile_by_profile_id_and_there_is_no_matching_profile : specification_for_profile_query_tasks
    {
        static Profile result;
        static int profile_id;
        static ProfileByIdSpecification specification;

        Establish context = () =>
        {
            profile_id = 1;

            the_profile_repository.StubFindOne().Return(null);
        };

        Because of = () => result = subject.GetProfileById(profile_id);

        It should_ask_the_repository_for_the_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByIdSpecification, Profile>(spec => spec.Id == profile_id);

        It should_return_null = () => result.ShouldBeNull();
    }
}
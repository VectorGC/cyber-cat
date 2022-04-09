using System;

namespace RestAPIWrapper
{
    [Flags]
    public enum WebError
    {
        NoError = 0,
        Internal = 1,
        MethodNotSupported = 2,

        EmailNotProvided = 100,
        EmailInvalid = 101,
        EmailUnknown = 102,
        EmailTaken = 103,

        LoginError = EmailNotProvided | MethodNotSupported
                                      | EmailNotProvided | EmailInvalid | EmailUnknown
                                      | PasswordNotProvided | PasswordInvalid | PasswordWrong,

        PasswordNotProvided = 200,
        PasswordInvalid = 201,
        PasswordWrong = 202,

        TokenNotProvided = 300,
        TokenInvalid = 301,
        TokenUnknown = 302,
        TokenNotVerified = 303,
        TokenBoundToOtherIP = 304,

        TaskIdNotProvided = 400,
        TaskIdInvalid = 401,
        TaskNotFound = 402,

        SolutionTextNotProvided = 500,
        SolutionTextTooLong = 501,
        SolutionTestsTooLong = 502,
        SolutionTestsInvalid = 503,
        SolutionBuildFail = 504,
        SolutionTestFail = 505,

        LanguageNotProvided = 600,
        LanguageNotSupported = 601,

        NameNotProvided = 700,
        NameInvalid = 701,

        TasksFoldersInvalid = 800,
        TasksProjectFolderNotFound = 801,
        TasksUnitFolderNotFound = 802,
        TasksTaskFolderNotFound = 803,

        LeaderboardProjectIdNotProvided = 900,
        LeaderboardProjectFolderNotProvided = 901
    }
}
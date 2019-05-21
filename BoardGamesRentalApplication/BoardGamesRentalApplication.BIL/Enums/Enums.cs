namespace BoardGamesRentalApplication.BIL.Enums
{
    public enum RegisterServiceResponse
    {
        SuccessRegister = 1,
        DuplicateUsername = 2,
        DuplicateEmail = 3
    }

    public enum LoginServiceResponse
    {
        LoginSuccessful,
        UserDoesntExist,
        IncorrectPassword
    }
}

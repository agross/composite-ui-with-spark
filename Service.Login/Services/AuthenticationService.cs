namespace Service.Login.Services
{
  class AuthenticationService : IAuthenticationService
  {
    public bool Authenticate(string user, string password)
    {
      return user == "admin" && password == "admin";
    }
  }
}
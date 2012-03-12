namespace Service.Login.Services
{
  public interface IAuthenticationService
  {
    bool Authenticate(string user, string password);
  }
}
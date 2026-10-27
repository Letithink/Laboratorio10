namespace AppLS10.application.Services;

public interface ITokenService
{
    string GenerateToken(Guid userId, string username, IEnumerable<string> roles);
}
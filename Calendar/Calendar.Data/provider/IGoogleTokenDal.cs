using Calendar.Models.GoogleCalendar;

namespace Calendar.DataAccess.Facade.provider
{
    public interface IGoogleTokenDal
    {
        Task<GoogleTokenResponse> GetTokenByIdAsync(Guid id);
        Task<IEnumerable<GoogleTokenResponse>> GetAllTokensAsync();
        Task AddTokenAsync(GoogleTokenResponse token);
        Task UpdateTokenAsync(GoogleTokenResponse token);
        Task DeleteTokenAsync(Guid id);
    }
}

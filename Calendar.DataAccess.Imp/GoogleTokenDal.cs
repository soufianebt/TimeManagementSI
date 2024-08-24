using Microsoft.EntityFrameworkCore;
using Calendar.Models.GoogleCalendar;
using Calendar.DataAccess.Facade.provider;

namespace Calendar.DataAccess.Imp
{
    public class GoogleTokenDal : IGoogleTokenDal
    {
        private readonly GoogleTokenContext _context;

        public GoogleTokenDal(GoogleTokenContext context)
        {
            _context = context;
        }

        public async Task<GoogleTokenResponse> GetTokenByIdAsync(Guid id)
        {
            return await _context.GoogleTokens.FindAsync(id);
        }

        public async Task<IEnumerable<GoogleTokenResponse>> GetAllTokensAsync()
        {
            return await _context.GoogleTokens.ToListAsync();
        }

        public async Task AddTokenAsync(GoogleTokenResponse token)
        {
            await _context.GoogleTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTokenAsync(GoogleTokenResponse token)
        {
            _context.GoogleTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTokenAsync(Guid id)
        {
            var token = await _context.GoogleTokens.FindAsync(id);
            if (token != null)
            {
                _context.GoogleTokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }
    }
}

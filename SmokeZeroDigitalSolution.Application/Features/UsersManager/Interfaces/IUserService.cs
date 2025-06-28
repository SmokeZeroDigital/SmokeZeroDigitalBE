namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> FindAsync(Guid id);
        IQueryable<AppUser> GetAll();
        IQueryable<AppUser> Get(Expression<Func<AppUser, bool>> where);
        IQueryable<AppUser> Get(Expression<Func<AppUser, bool>> where, params Expression<Func<AppUser, object>>[] includes);
        IQueryable<AppUser> Get(Expression<Func<AppUser, bool>> where, Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include = null);
        Task AddAsync(AppUser user);
        Task AddRangce(IEnumerable<AppUser> users);
        void Update(AppUser user);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<AppUser, bool>> where);
        Task<bool> SaveChangeAsync();
        object GetUserProfileFromToken(ClaimsPrincipal user);
    }
}
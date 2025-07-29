namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Services
{
    public class UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, UserManager<AppUser> userManager, IJWTService jWTService) : IUserService
    {
        private IUnitOfWork _unitOfWork = unitOfWork;
        private IUserRepository _userRepository = userRepository;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IJWTService _jwtService = jWTService;
        public async Task AddAsync(AppUser user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task AddRangce(IEnumerable<AppUser> users)
        {
            await _userRepository.AddRangce(users);
        }

        public async Task<AppUser> FindAsync(Guid id)
        {
            return await _userRepository.FindAsync(id);
        }

        public IQueryable<AppUser> GetAll()
        {
            return _userRepository.GetAll();
        }

        public IQueryable<AppUser> Get(Expression<Func<AppUser, bool>> where)
        {
            return _userRepository.Get(where);
        }

        public IQueryable<AppUser> Get(Expression<Func<AppUser, bool>> where, params Expression<Func<AppUser, object>>[] includes)
        {
            return _userRepository.Get(where, includes);
        }

        public IQueryable<AppUser> Get(Expression<Func<AppUser, bool>> where, Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include = null)
        {
            return _userRepository.Get(where, include);
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _userRepository.Remove(id);
        }

        public async Task<bool> CheckExist(Expression<Func<AppUser, bool>> where)
        {
            return await _userRepository.CheckExist(where);
        }

        public void Update(AppUser user)
        {
            _userRepository.Update(user);
        }
    }
}

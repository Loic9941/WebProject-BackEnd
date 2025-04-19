using BLL.IService;
using DAL.Repository;
using Domain;

namespace BLL.Services
{
    public class RatingService : IRatingService
    {
        protected readonly IGenericRepository<Rating> _ratingRepository;
        protected readonly IAuthenticationService _authenticationService;

        public RatingService(
            IGenericRepository<Rating> ratingRepository,
            IAuthenticationService authenticationService
            )
        {
            _ratingRepository = ratingRepository;
            _authenticationService = authenticationService;
        }

        public IEnumerable<Rating> GetRatings()
        {
            return _ratingRepository.Get(
                r => r.InvoiceItem.Product.UserId == _authenticationService.GetUserId(),
                null,
                "InvoiceItem"
                );
        }
    }
}

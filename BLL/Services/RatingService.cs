using System.Linq.Expressions;
using BLL.IService;
using DAL.Repository;
using Domain;
using LinqKit;

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

        public IEnumerable<Rating> GetRatings(int? productId)
        {
            Expression<Func<Rating, bool>>? filter = PredicateBuilder.New<Rating>(true);
            if (productId != null)
            {
                filter = filter.And(x => x.InvoiceItem.ProductId == productId);
            }
            if (_authenticationService.IsArtisan())
            {
                filter = filter.And(x => x.InvoiceItem.Product.UserId == _authenticationService.GetUserId());
                return _ratingRepository.Get(
                    filter,
                    null,
                    "InvoiceItem,InvoiceItem.User"
                );
            }
            else if (_authenticationService.IsCustomer())
            {
                return _ratingRepository.Get(
                    filter,
                    null,
                    "InvoiceItem,InvoiceItem.User"
                );
            }
            else
            {
                throw new Exception("You are not authorized to access this rating");
            }

        }
    }
}

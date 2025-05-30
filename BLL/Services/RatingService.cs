using System.Linq.Expressions;
using BLL.DTOs.InputDTOs;
using BLL.IServices;
using DAL.IRepositories;
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
                    "InvoiceItem,InvoiceItem.User,Comment"
                );
            }
            else if (_authenticationService.IsCustomer() || _authenticationService.IsAdmin())
            {
                return _ratingRepository.Get(
                    filter,
                    null,
                    "InvoiceItem,InvoiceItem.User,Comment"
                );
            }
            else
            {
                throw new Exception("You are not authorized to access this rating");
            }
        }

        public Rating? GetRating(int id)
        {
            return _ratingRepository.GetSingleOrDefault(
                x => x.Id == id,
                "InvoiceItem,InvoiceItem.User,Comment"
            );
        }

        public void SaveComment(int id, int? commentId, CommentDTO commentDTO)
        {
            var rating = _ratingRepository.GetSingleOrDefault(
                x => x.Id == id, "Comment"
            ) ?? throw new Exception("Rating not found");
            if (_authenticationService.IsArtisan())
            {
                if (rating.Comment is null)
                {
                    rating.Comment = new Comment
                    {
                        Text = commentDTO.Text,
                        RatingId = rating.Id,
                    };
                    _ratingRepository.Update(rating);
                }
                else
                {
                    rating.Comment.Text = commentDTO.Text;
                }
                
            }
            else
            {
                throw new Exception("You are not authorized to add a comment to this rating");
            }
        }
    }
}

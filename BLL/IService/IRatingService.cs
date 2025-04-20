using BLL.DTOs.InputDTOs;
using Domain;

namespace BLL.IService
{
    public interface IRatingService
    {
        public IEnumerable<Rating> GetRatings(int? productId);

        public Rating? GetRating(int id);

        public void AddComment(int id, CommentDTO commentDTO);
    }
}

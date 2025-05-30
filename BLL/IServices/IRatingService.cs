using BLL.DTOs.InputDTOs;
using Domain;

namespace BLL.IServices
{
    public interface IRatingService
    {
        public IEnumerable<Rating> GetRatings(int? productId);

        public Rating? GetRating(int id);

        public void SaveComment(int id, int? commentId, CommentDTO commentDTO);
    }
}

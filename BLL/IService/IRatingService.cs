using Domain;

namespace BLL.IService
{
    public interface IRatingService
    {
        public IEnumerable<Rating> GetRatings();
    }
}

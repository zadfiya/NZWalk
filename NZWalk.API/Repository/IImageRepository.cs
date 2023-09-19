using NZWalk.API.Model.Domain;

namespace NZWalk.API.Repository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

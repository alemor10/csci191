using System.Collections.Generic;
using System.Threading.Tasks;
using SocialApp.Models;

namespace SocialApp.Services
{
    public interface IPicturePostStore
    {
        Task<IEnumerable<PicturePost>> GetPicturePostsAsync();

        Task<PicturePost> GetPicturePost(int id);

        Task AddPicturePost(PicturePost posr);

        Task UpdatePicturePost(PicturePost post);

        Task DeletePicturePost(PicturePost post);

    }
}
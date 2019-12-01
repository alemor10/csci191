using System.Collections.Generic;
using System.Threading.Tasks;


namespace SocialApp.Services
{
    interface IPicturePostService

    {
        Task<IEnumerable<Models.PicturePost>> GetPosts();
        Task UpdatePosts();
    }
}

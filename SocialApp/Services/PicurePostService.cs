using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;
using SocialApp.Models;

namespace SocialApp.Services
{
    public class PicturePostService : IPicturePostService
    {
        private IEnumerable<Models.PicturePost> _pictureposts;
        private readonly HttpClient _httpClient;

        private const string PicturePostFolder = "PicturePost";
        private const string PicturePostsFileName = "picturePost.json";

        public PicturePostService()
        {
            _httpClient = new HttpClient();
        }

        public Task<IEnumerable<PicturePost>> GetPosts()
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePosts()
        {
            // URL should point to where your service is running
            const string uri = "http://offlinestorageserver.azurewebsites.net/api/values";
            var httpResult = await _httpClient.GetAsync(uri);
            var jsonCompanies = await httpResult.Content.ReadAsStringAsync();

            var picturePosts = JsonConvert.DeserializeObject<ICollection<Models.PicturePost>>(jsonCompanies);

            var folder = await NavigateToFolder(PicturePostFolder);
            //await StoreImagesLocallyAndUpdatePath(folder, companies);
            await SerializePicturePost(folder, picturePosts);

            _pictureposts = picturePosts;
        }

        private async Task SerializePicturePost(IFolder folder, ICollection<PicturePost> companies)
        {
            IFile file = await folder.CreateFileAsync(PicturePostsFileName, CreationCollisionOption.ReplaceExisting);
            var picturePostString = JsonConvert.SerializeObject(companies);
            await file.WriteAllTextAsync(picturePostString);
        }

        private static async Task<IFolder> NavigateToFolder(string picturePostFolder)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(picturePostFolder,
                CreationCollisionOption.OpenIfExists);

            return folder;
        }
    }
}

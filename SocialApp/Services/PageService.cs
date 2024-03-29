﻿
using System.Threading.Tasks;
using Xamarin.Forms;

using SocialApp.Services;

//interface for PageService
namespace SocialApp.Services
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();

        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task DisplayAlert(string title, string message, string ok);
    }
}
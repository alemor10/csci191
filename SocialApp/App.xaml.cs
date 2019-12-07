using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SocialApp.Models;
using SQLite; 

namespace SocialApp
{


    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }


        static postDatabase database;
        public static postDatabase Database
        {
          get
          {
            if (database == null)
            {
              database = new postDatabase(
              Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "postSQLite.db3"));
            }
            return database;
          }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

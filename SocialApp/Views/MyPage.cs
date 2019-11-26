using System;
using System.Collections.Generic;
using SocialApp.Models;
using SocialApp.ModelViews;
using Xamarin.Forms;

using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace SocialApp.Views
{
    public class MyPage : ContentPage
    {
        
        void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    App.Current.MainPage.DisplayAlert("Notification", "Successfully Login", "Left1");
                    break;
                case SwipeDirection.Right:
                    App.Current.MainPage.DisplayAlert("Notification", "Successfully Login", "Right2");
                    break;
            }
        }




        [Obsolete]
        public MyPage()
        {



            BackgroundColor = Color.PowderBlue;
            BindingContext = new PicturesViewModel();

            string imagePath = "";

            var image = new Image { Source = "waterfront.jpg" };
            var pickImageButton = new Button
            {
                Text = "Pick Image",
                TextColor = Color.Black,
                BackgroundColor = Color.Gray,
                Margin = new Thickness(15)

            };
            pickImageButton.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (file == null)
                    return;

                imagePath = file.Path.ToString();

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });




                file.Dispose();


            };


            var categoryList = new List<string>();
            categoryList.Add("Business");
            categoryList.Add("Educational");
            categoryList.Add("Personal");
            categoryList.Add("Rated R");
            categoryList.Add("Waifus");


            Entry titleEntry = new Entry
            {
                Placeholder = "HEWWO",
                Margin = new Thickness(10)

            };
            titleEntry.SetBinding(Entry.TextProperty, nameof(PicturesViewModel.PictureTitle));


            var ratingLabel = new Label
            {
                BackgroundColor = Color.Aquamarine
            };
            ratingLabel.SetBinding(Label.TextProperty, nameof(PicturesViewModel.PictureRating));

            var categoryPicker = new Picker
            {
                Title = "Category",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            categoryPicker.ItemsSource = categoryList;
            categoryPicker.SetBinding(Picker.SelectedItemProperty, nameof(PicturesViewModel.PictureCategory));


            var collectionView = new CollectionView
            {
                ItemTemplate = new NotesTemplate()
            };
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(PicturesViewModel.Posts));

            var saveButton = new Button
            {
                Text = "Save",
                TextColor = Color.Black,
                BackgroundColor = Color.Gray,
                Margin = new Thickness(15)

            };
            saveButton.SetBinding(Button.CommandProperty, nameof(PicturesViewModel.SavePostCommand));

            var ratingView = new BoxView { Color = Color.Teal };
            var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            leftSwipeGesture.Swiped += OnSwiped;
            var rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            rightSwipeGesture.Swiped += OnSwiped;
            ratingView.GestureRecognizers.Add(leftSwipeGesture);
            ratingView.GestureRecognizers.Add(rightSwipeGesture);

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" },
                    image,
                    pickImageButton,
                    titleEntry,
                    categoryPicker,
                    ratingLabel,
                    ratingView,
                    saveButton,
                    collectionView
                    
                }
            };
        }

        class NotesTemplate : DataTemplate
        {
            public NotesTemplate() : base(LoadTemplate)
            {

            }

            static StackLayout LoadTemplate()
            {
                var textLabel = new Label();
                textLabel.SetBinding(Label.TextProperty, nameof(PicturePost.PictureTitle));

                var pictureCategory = new Label();
                pictureCategory.SetBinding(Label.TextProperty, nameof(PicturePost.PictureCategory));


                var frame = new Frame
                {
                    BorderColor = Color.AntiqueWhite,
                    Padding = 3,
                    VerticalOptions = LayoutOptions.Center,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            textLabel,
                            pictureCategory
                        }
                    }
                    
                };

                return new StackLayout
                {
                    Children = { frame },
                    Padding = new Thickness(10)
                };
            }
        }
    }
}


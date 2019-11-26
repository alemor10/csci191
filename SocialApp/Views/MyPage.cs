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


        [Obsolete]
        public MyPage()
        {
            BackgroundColor = Color.PowderBlue;
            BindingContext = new PicturesViewModel();

            string imagePath = "";
            double imageRating = 0.0;

            var ratingLabel = new Editor
            {
                BackgroundColor = Color.Aquamarine,
                IsReadOnly = true,
                Keyboard = Keyboard.Numeric
            };
            ratingLabel.SetBinding(Editor.TextProperty, nameof(PicturesViewModel.PictureRating));

            var ratingView = new BoxView { Color = Color.Teal };
            var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            leftSwipeGesture.Swiped += OnSwiped;
            var rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            rightSwipeGesture.Swiped += OnSwiped;
            ratingView.GestureRecognizers.Add(leftSwipeGesture);
            ratingView.GestureRecognizers.Add(rightSwipeGesture);

            void OnSwiped(object sender, SwipedEventArgs e)
            {
                switch (e.Direction)
                {
                    case SwipeDirection.Left:
                        imageRating -= 0.5;
                        var tempLeft = imageRating.ToString();
                        ratingLabel.SetValue(Editor.TextProperty, tempLeft);
                        break;
                    case SwipeDirection.Right:
                        imageRating += 0.5;
                        var tempRight = imageRating.ToString();
                        ratingLabel.SetValue(Editor.TextProperty, tempRight);
                        break;
                }
            }


            var image = new Image { Aspect= Aspect.AspectFit };

            var filePath = new Editor
            { 
               IsReadOnly = true
            };
            filePath.SetBinding(Editor.TextProperty, nameof(PicturesViewModel.PicturePath));
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

                //filePath.SetBinding(Editor.TextProperty, nameof(PicturesViewModel.PicturePath));
                filePath.SetValue(Editor.TextProperty, imagePath);
              

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
                Placeholder = "Title",
                Margin = new Thickness(10)

            };
            titleEntry.SetBinding(Entry.TextProperty, nameof(PicturesViewModel.PictureTitle));


            var categoryPicker = new Picker
            {
                Title = "Category",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            categoryPicker.ItemsSource = categoryList;
            categoryPicker.SetBinding(Picker.SelectedItemProperty, nameof(PicturesViewModel.PictureCategory));


            var saveButton = new Button
            {
                Text = "Save",
                TextColor = Color.Black,
                BackgroundColor = Color.Gray,
                Margin = new Thickness(15)

            };
            saveButton.SetBinding(Button.CommandProperty, nameof(PicturesViewModel.SavePostCommand));

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Content = new StackLayout
            {
                Children = {
                    image,
                    filePath,
                    pickImageButton,
                    titleEntry,
                    filePath,
                    categoryPicker,
                    ratingLabel,
                    ratingView,
                    saveButton,
                    
                }
            };
        }

     
    }
}


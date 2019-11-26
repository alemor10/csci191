using System;
using SocialApp.Models;
using SocialApp.ModelViews;
using Xamarin.Forms;

namespace SocialApp.Views
{
    public class CollectionPage : ContentPage
    {
        [Obsolete]
        public CollectionPage()
        {

            var collectionView = new CollectionView
            {
                ItemTemplate = new NotesTemplate()
            };
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(PicturesViewModel.Posts));

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "BUBBHEAD RULES " },
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
                var titleLabel = new Label();
                titleLabel.SetBinding(Label.TextProperty, nameof(PicturePost.PictureTitle));

                var pictureCategory = new Label();
                pictureCategory.SetBinding(Label.TextProperty, nameof(PicturePost.PictureCategory));

                var picturePathLabel = new Label();
                picturePathLabel.SetBinding(Label.TextProperty, nameof(PicturePost.PicturePath));

                var pictureRatingLabel = new Label();
                pictureRatingLabel.SetBinding(Label.TextProperty, nameof(PicturePost.PictureRating));

                Frame frame = new Frame
                {
                    BorderColor = Color.AntiqueWhite,
                    Padding = 3,
                    VerticalOptions = LayoutOptions.Center,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            titleLabel,
                            pictureRatingLabel,
                            picturePathLabel,
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


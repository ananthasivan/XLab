using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SampleListViewPOC
{
    public class App
    {
        public static INavigation Navigator { get; set; }

        public static Page GetMainPage()
        {

            Button btnGo = new Button
            {
                Text = "Go",
                TextColor = Color.Black
            };

            btnGo.Clicked += btnGo_Clicked;

            StackLayout mainLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Silver,
                Children =
                {
                    new Label
                    {
                        Text = "Welcome to List View Sample",
                        TextColor = Color.Black,
                    },
                    btnGo
                }
            };

            var firstPage = new NavigationPage(new ContentPage
            {
                BackgroundColor = Color.White,
                Content = mainLayout
            });

            Navigator = firstPage.Navigation;

            return firstPage;
        }

        #region Button Click Handler
        static void btnGo_Clicked(object sender, EventArgs e)
        {
            Navigator.PushAsync(new ActionIconListView());
        }
        #endregion

        
    }

    #region CLASS - BASEMODEL
    /// <summary>
    /// Class BaseModel.
    /// </summary>
    public class BaseModel
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModel"/> class.
        /// </summary>
        public BaseModel()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Id field
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Name field
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        #endregion
    }
    #endregion

    #region CLASS - ACTION ICON LIST
    public class ActionIconList : BaseModel
    {
        public string LeftIconName { get; set; }

        public string Title { get; set; }
    }
    #endregion 

    #region Class ActionIconListView.
    /// <summary>
    /// Class ActionIconListView.
    /// </summary>
    public class ActionIconListView : ContentPage
    {
        #region PRIVATE VARIABLES
        ListView listView = null;
        System.Collections.ObjectModel.ObservableCollection<ActionIconList> actionIconListItems = null;
        IDeviceSpecs spec = DependencyService.Get<IDeviceSpecs>();
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionIconListView" /> class.
        /// </summary>
        public ActionIconListView()
        {

            #region LIST ITEMS

            listView = new ListView();
            listView.ItemTemplate = new DataTemplate(typeof(ActionIconListCell));
            actionIconListItems = new System.Collections.ObjectModel.ObservableCollection<ActionIconList>();

            actionIconListItems.Add(new ActionIconList
            {
                LeftIconName = "rtemp.png",
                Title = "Red"
            });

            actionIconListItems.Add(new ActionIconList
            {
                LeftIconName = "gtemp.png",
                Title = "Green"
            });

            actionIconListItems.Add(new ActionIconList
            {
                LeftIconName = "ytemp.png",
                Title = "Yellow"
            });

            actionIconListItems.Add(new ActionIconList
            {
                LeftIconName = "bltemp.png",
                Title = "sky blue"
            });

            actionIconListItems.Add(new ActionIconList
            {
                LeftIconName = "brtemp.png",
                Title = "Brown"
            });

            listView.ItemsSource = actionIconListItems;
            

            listView.HasUnevenRows = true;
            listView.BackgroundColor = Color.White;
            listView.ItemTapped += listView_ItemTapped;

            #endregion


            #region MAIN CONTENT

            this.BackgroundColor = Color.White; 

            this.Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                Children = {
                    new StackLayout
                    {
                        Padding = 10,
                        Children = { listView }
                    }
                }
            };
            #endregion
        }

        #endregion

        #region LIST VIEW TAPPED EVENT HANDLER

        void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ActionIconList selectedActionList = e.Item as ActionIconList;
            //navigate to => respective item

            Label lblTitle = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = selectedActionList.Title,
                TextColor = Color.Black,
                BackgroundColor = Color.Blue
            };


            HybridWebView webView = new HybridWebView
            {
                WidthRequest = spec.ScreenWidth,

                HeightRequest = spec.ScreenHeight,
            };

            HtmlWebViewSource htmlSource = new HtmlWebViewSource
                 {
                     Html = @"<html>
                                <head>
                                <title>Travel FAQs</title>
                                <style>
                                li{
                                     padding:0px 0px 10px 0px;
                                }
                                body {
                                 -webkit-overflow-scrolling: touch;    
                                }
                                </style>
                                </head>
                                <body>
                                <div style='overflow:auto;height:" + GetDivScrollHeight()+ @";'>
                                <ul>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                <li>This is a paragraph.</li>
                                </ul>
                                </div>
                                </body>
                                </html>"
                 };

            webView.Source = htmlSource;

            if (Device.OS != TargetPlatform.iOS)
            {
                htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            }

            StackLayout sLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White,
                Children =
                {
                    lblTitle,
                    webView
                }
            };

            ContentPage c = new ContentPage
            {
                Content = sLayout
            };

            c.BackgroundColor = Color.Silver;

            App.Navigator.PushAsync(c);
        }
        #endregion

        protected override void OnBindingContextChanged()
        {
            listView.BindingContext = this.BindingContext;
        }

        public static double GetDivScrollHeight()
        {
            IDeviceSpecs specs = DependencyService.Get<IDeviceSpecs>();
            double scrollHeight = specs.ScreenHeight;

            if (Device.OS == TargetPlatform.iOS)
            {
                scrollHeight = specs.ScreenHeight * 85 / 100;
            }

            if (Device.OS == TargetPlatform.Android)
            {
                scrollHeight = (specs.ScreenHeight / specs.ScreenDensity) * 82.5 / 100;
            }

            return scrollHeight;
        }
    }

    #endregion

    #region CUSTOM ICON LIST CELL
    class ActionIconListCell : ViewCell
    {
        const int maxCharsInRowForIos = 25;
        const int defaultHeight = 50;
        const int extraLineHeight = 20;

        public ActionIconListCell()
        {
            Label nameLabel = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                TextColor = Color.FromHex("505050")
            };
            nameLabel.SetBinding(Label.TextProperty, "Title");


            Image leftIcon = new Image();
            leftIcon.SetBinding(Image.SourceProperty, "LeftIconName");

            StackLayout viewLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, 15, 5, 10),
                Children = { leftIcon, nameLabel }
            };

            BoxView linebv = new BoxView() { HeightRequest = 1, Color = Color.FromHex("cfcfcf") };
            StackLayout lstLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { viewLayout }
            };

            if (Device.OS == TargetPlatform.Android)
            {
                lstLayout.Children.Add(linebv);
            }

            View = lstLayout;
        }

        
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Device.OS == TargetPlatform.iOS)
            {
                var actionList = (ActionIconList)BindingContext;

                var len = actionList.Title.Length;
                if (len <= maxCharsInRowForIos)
                {
                    Height = defaultHeight;
                }
                else
                {
                    len = len - maxCharsInRowForIos; 
                    var extraRows = len / maxCharsInRowForIos;
                    if ((len % maxCharsInRowForIos) > 0)
                        extraRows += 1;

                    Height = defaultHeight + extraRows * extraLineHeight;
                }
            }
        }
    }
    #endregion


    #region HYBRID WEB VIEW
    public class HybridWebView : WebView { }
    #endregion

    public interface IBaseUrl { string Get(); }

    #region Device Specs interface
    public interface IDeviceSpecs
    {
        /// <summary>
        /// Gets the width of the screen.
        /// </summary>
        /// <value>The width of the screen.</value>
        double ScreenWidth { get; }
        /// <summary>
        /// Gets the height of the screen.
        /// </summary>
        /// <value>The height of the screen.</value>
        double ScreenHeight { get; }
        /// <summary>
        /// Gets the screen density.
        /// </summary>
        /// <value>The screen density.</value>
        double ScreenDensity { get; }
    }
    #endregion

   

}

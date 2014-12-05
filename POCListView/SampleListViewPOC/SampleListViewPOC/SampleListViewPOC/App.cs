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

    /// <summary>
    /// Class ActionIconListView.
    /// </summary>
    public class ActionIconListView : ContentPage
    {
        #region PRIVATE VARIABLES
        ListView listView = null;
        System.Collections.ObjectModel.ObservableCollection<ActionIconList> actionIconListItems = null;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionIconListView" /> class.
        /// </summary>
        public ActionIconListView()
        {

            #region LIST ITEMS

            listView = new ListView();
            //listView.SetBinding(ListView.ItemsSourceProperty, "ListContent");
            listView.ItemTemplate = new DataTemplate(typeof(ActionIconListCell));
            
            //listView.ItemsSource = HomeEmergencyManager.GetActionListInfoForHomeEmergencyExclusions().ActionListItems;

            actionIconListItems = new System.Collections.ObjectModel.ObservableCollection<ActionIconList>();

            //actionIconListItems.Add(new ActionIconList
            //{
            //    LeftIconName = "sample_spacer.png",
            //    Title = "Sample List"
            //});

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

            ContentPage c = new ContentPage
            {
                Content = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = selectedActionList.Title,
                    TextColor = Color.Black,
                    BackgroundColor = Color.White
                }
            };

            c.BackgroundColor = Color.Silver;

            App.Navigator.PushAsync(c);
        }
        #endregion

        protected override void OnBindingContextChanged()
        {
            listView.BindingContext = this.BindingContext;
        }

        
    }

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

}

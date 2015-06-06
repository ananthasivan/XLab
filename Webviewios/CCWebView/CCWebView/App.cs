using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CCWebView
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new ContentPage
            {
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                    Padding = new Thickness(0,20,0,0),
                    Children =
                    {
                        new Label
                        {
                            Text = "I am  a Label"
                        },
                        new HybridWebView
                        {
                            Source = new HtmlWebViewSource
                            {
                                Html = @"<html>
                                            <body>
                                                <h1>Xamarin.Forms</h1>
                                                <p>Helloworld!</p>
                                                <p><a href=""local.html"">next page</a></p>
                                                <br>
                                                Link : <a href='http://www.google.com'>Hello</a>
                                            </body>
                                            </html>",
                            },
                            WidthRequest = 300,
                            HeightRequest = 200
                        }
                    }

                },
            };
        }
    }
}

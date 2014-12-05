using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Webkit;
using Android.Content;
using SampleListViewPOC;
using SampleListViewPOC.Droid;

[assembly: Xamarin.Forms.ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
[assembly: Dependency(typeof(BaseUrl_Android))]
[assembly: Dependency(typeof(DeviceSpecs_Android))]
namespace SampleListViewPOC.Droid
{
    [Activity(Label = "SampleListViewPOC", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            SetPage(App.GetMainPage());
        }
    }

    #region Renderer for Hybrid Web View
    public class HybridWebViewRenderer : WebRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                // lets get a reference to the native control
                var webView = (global::Android.Webkit.WebView)Control;
                webView.SetWebViewClient(new CCWebViewClient());
                //webView.SetInitialScale(-1);
                webView.Settings.JavaScriptEnabled = true;
            }
        }

        #region Web View Client
        public class CCWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, string url)
            {
                var uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionView, uri);
                Forms.Context.StartActivity(intent);
                return true;
            }
        }
        #endregion

        #region JavaScript Result
        public class JavaScriptResult : Java.Lang.Object, IValueCallback
        {
            public void OnReceiveValue(Java.Lang.Object val)
            {
                Console.WriteLine("Returned value: {0}", val);
            }
        }
        #endregion
    }

    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_res/drawable/";
            //return "file:///android_asset/";
        }
    }
    #endregion

    #region Device specs
    public class DeviceSpecs_Android : IDeviceSpecs
    {
        /// <summary>
        /// Gets the width of the screen.
        /// </summary>
        /// <value>The width of the screen.</value>
        public double ScreenWidth
        {
            get
            {
                return Application.Context.Resources.DisplayMetrics.WidthPixels;
            }

        }

        /// <summary>
        /// Gets the height of the screen.
        /// </summary>
        /// <value>The height of the screen.</value>
        public double ScreenHeight
        {
            get
            {
                return Application.Context.Resources.DisplayMetrics.HeightPixels;
            }

        }

        /// <summary>
        /// Gets the screen density.
        /// </summary>
        /// <value>The screen density.</value>
        public double ScreenDensity
        {
            get
            {
                return Application.Context.Resources.DisplayMetrics.Density;
            }

        }
    }
    #endregion

    
}


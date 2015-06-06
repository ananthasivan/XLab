using System;
using Xamarin.Forms.Platform.Android;
using Android.Webkit;
using CCWebView.Droid;
using CCWebView;
using Xamarin.Forms;
using Android.Content;

[assembly: Xamarin.Forms.ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
[assembly: Dependency(typeof(BaseUrl_Android))]
namespace CCWebView.Droid
{
    public class HybridWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                // lets get a reference to the native control
                var webView = (global::Android.Webkit.WebView)Control;
                webView.SetWebViewClient(new MyWebViewClient());
                webView.Settings.JavaScriptEnabled = true;
            }
        }

        public class MyWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, string url)
            {
                var uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionView, uri);
                Forms.Context.StartActivity(intent);
                return true;
            }
        }

        public class JavaScriptResult : Java.Lang.Object, IValueCallback
        {
            public void OnReceiveValue(Java.Lang.Object val)
            {
                Console.WriteLine("Returned value: {0}", val);
            }
        }
    }

    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_res/drawable/";
        }
    }
}
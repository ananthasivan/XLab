
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CCWebView.iOS;
using CCWebView;

[assembly: Dependency(typeof(BaseUrl_iOS))]
[assembly: Xamarin.Forms.ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]

namespace CCWebView.iOS
{
    public class HybridWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {   // perform initial setup
                // lets get a reference to the native control
                var webView = (UIWebView)this.NativeView;
                webView.ScalesPageToFit = true;
                webView.ShouldStartLoad += HandleStartLoad; 
            }
        }

        private bool HandleStartLoad(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
        {
            if (navigationType == UIWebViewNavigationType.LinkClicked)
            {
                UIApplication.SharedApplication.OpenUrl(request.Url);
                return true;
            }

            return false;
        }

        public override void LoadHtmlString(string s, NSUrl baseUrl)
        {
            if (baseUrl == null)
            {
                baseUrl = new NSUrl(NSBundle.MainBundle.BundlePath, true);
            }

            base.LoadHtmlString(s, baseUrl);
        }
    }

    public class BaseUrl_iOS : IBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}
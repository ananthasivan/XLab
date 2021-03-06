﻿using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using SampleListViewPOC;
using SampleListViewPOC.iOS;

[assembly: Dependency(typeof(BaseUrl_iOS))]
[assembly: Dependency(typeof(DeviceSpecs_IOS))]
[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace SampleListViewPOC.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }

    #region Renderer for Hybrid Web View
    public class HybridWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {   // perform initial setup
                // lets get a reference to the native control
                var webView = (UIWebView)this.NativeView;
                //webView.ScalesPageToFit = true;
                webView.ShouldStartLoad += HandleStartLoad;
            }
        }

        private bool HandleStartLoad(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
        {
            if (navigationType == UIWebViewNavigationType.LinkClicked)
            {
                UIApplication.SharedApplication.OpenUrl(request.Url);
                return false;
            }

            return true;
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
    #endregion

    public class DeviceSpecs_IOS : IDeviceSpecs
    {


        /// <summary>
        /// Gets the width of the screen.
        /// </summary>
        /// <value>The width of the screen.</value>
        public double ScreenWidth
        {
            get { return UIScreen.MainScreen.Bounds.Width; }
        }

        /// <summary>
        /// Gets the height of the screen.
        /// </summary>
        /// <value>The height of the screen.</value>
        public double ScreenHeight
        {
            get { return UIScreen.MainScreen.Bounds.Height; }
        }

        /// <summary>
        /// Gets the screen density.
        /// </summary>
        /// <value>The screen density.</value>
        public double ScreenDensity
        {
            get { return 0; }

        }


    }
}

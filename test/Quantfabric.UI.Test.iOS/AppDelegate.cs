﻿using System;
using Foundation;
using UIKit;

namespace Quantfabric.UI.Test.iOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Material.Framework.Platform.Current.Initialize();

            return true;
        }

        public override void OnActivated(UIApplication application)
        {
        }

        public override void PerformFetch(
            UIApplication application,
            Action<UIBackgroundFetchResult> completionHandler)
        {

            completionHandler(UIBackgroundFetchResult.NewData);
        }

        public override void DidEnterBackground(UIApplication application)
        {

        }

	    public override bool OpenUrl(
            UIApplication app,
            NSUrl url, 
            NSDictionary options)
	    {
            //necessary for custom uri scheme OAuth callbacks to function
            Material.Framework.Platform.Current.Protocol(url);

            return true;
	    }

	    public override UIWindow Window
		{
			get;
			set;
		}
    }
}



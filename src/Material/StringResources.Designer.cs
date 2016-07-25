﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Material {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class StringResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StringResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Material.StringResources", typeof(StringResources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Received code {0} instead of 200. Body content : {1}.
        /// </summary>
        public static string BadHttpRequestException {
            get {
                return ResourceManager.GetString("BadHttpRequestException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not connect to bluetooth device at address {0}.
        /// </summary>
        public static string BluetoothConnectivityException {
            get {
                return ResourceManager.GetString("BluetoothConnectivityException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given type is not a subclass of Event.
        /// </summary>
        public static string GenericTypeException {
            get {
                return ResourceManager.GetString("GenericTypeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GPS device is not enabled.
        /// </summary>
        public static string GPSDisabledConnectivityException {
            get {
                return ResourceManager.GetString("GPSDisabledConnectivityException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not get a GPS lock in the given timeout period.
        /// </summary>
        public static string GPSTimeoutConnectivityException {
            get {
                return ResourceManager.GetString("GPSTimeoutConnectivityException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must provide a Client Secret; token flow is not supported with the service {0}.
        /// </summary>
        public static string GrantTypeNotSupportedException {
            get {
                return ResourceManager.GetString("GrantTypeNotSupportedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thanks for sharing!.
        /// </summary>
        public static string OAuthCallbackResponse {
            get {
                return ResourceManager.GetString("OAuthCallbackResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The HTTP request cannot be made because this device is not connected to the internet.
        /// </summary>
        public static string OfflineConnectivityException {
            get {
                return ResourceManager.GetString("OfflineConnectivityException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The requested scope {0} is not valid for the resource provider {1}.
        /// </summary>
        public static string ScopeException {
            get {
                return ResourceManager.GetString("ScopeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The state parameter returned by the resource provider was &apos;{0}&apos;.
        /// </summary>
        public static string StateParameterInvalidException {
            get {
                return ResourceManager.GetString("StateParameterInvalidException", resourceCulture);
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTestTask.Configuration
{
    
    [JsonObject(ItemRequired = Required.Always)]
    public class Config
    {
        [JsonProperty(Required = Required.Always)]
        public IOS IOS;

        [JsonProperty(Required = Required.Always)]
        public Android Android;
    }

    #region iOS
    [JsonObject(ItemRequired = Required.Always)]
    public class IOS
    {
        public Capabilities Capabilities;
        public IOSSearchProductsTest IOSSearchProductsTest;
        public IOSAuthorizeTest IOSAuthorizeTest;
        public IOSSwitchAppToBrowserAndBackToAppTest IOSSwitchAppToBrowserAndBackToAppTest;
    }

    [JsonObject(ItemRequired = Required.Always)]
    public class Capabilities
    {
        public string Hub;
        public string DeviceName;
        public string PlatformName;
        public string Udid;
        public string BundleId;
        public string AutomationName;
        public string NoReset;
        public string UseNewWDA;
        public string PlatformVersion;
        public string NewCommandTimeout;
        public string Language;

    }

    [JsonObject(ItemRequired = Required.Always)]
    public class IOSSearchProductsTest
    {
        public string MenuElementForClick;
        public string TypeSearchLine;               
    }

    [JsonObject(ItemRequired = Required.Always)]
    public class IOSAuthorizeTest
    {
        public IsLoggined IsLoggined;
        public Authorize Authorize;
        public LogOut LogOut;
    }

    [JsonObject(ItemRequired = Required.Always)]
    public class IsLoggined
    {
        public string menuElementForClick;
        public string userIsLoginnedAttribute;

    }

    [JsonObject(ItemRequired = Required.Always)]
    public class Authorize
    {
        public string MenuElementForClick1;
        public string MenuElementForClick2;
        public string Login;
        public string Pass;
    }

    [JsonObject(ItemRequired = Required.Always)]
    public class LogOut
    {
        public string MenuElementForClick1;
        public string MenuElementForClick2;
        public string ElementExistInMenu;
    }

    [JsonObject(ItemRequired = Required.Always)]
    public class IOSSwitchAppToBrowserAndBackToAppTest
    {
        public string MenuElementForClick;
       
    }
    #endregion

    #region Android
    [JsonObject(ItemRequired = Required.Always)]
    public class Android
    {
        public int CapabilitiesItem;
        public AndroidCapabilities[] AndroidCapabilitiesList;
        public InstagramCredential InstagramCredential;
        public string PhotoStorageOnAndroid;
        public string PhotoSourceOnHost;
    }

    [JsonObject(ItemRequired = Required.Always)]
    public class AndroidCapabilities
    {
        public string Hub;
        public string DeviceName;
        public string PlatformName;
        public string PlatformVersion;
        public string Udid;
        public string NewCommandTimeout;        
        public string AutomationName;

        public string adbExecTimeout;

        public string appPackage;
        public string appActivity;
        public string app;
        public bool autoDismissAlerts;
    }

    [JsonObject(ItemRequired = Required.Always)]
    public class InstagramCredential
    {
        public string login;
        public string password;
    }

    #endregion

}

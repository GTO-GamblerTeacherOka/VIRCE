using System;
using System.Threading;
using Pixiv.VroidSdk;
using Pixiv.VroidSdk.Api;
using Pixiv.VroidSdk.Browser;
using Pixiv.VroidSdk.Oauth;
using Pixiv.VroidSdk.Oauth.DataModel;
using UnityEngine;

namespace VRoid
{
    /// <summary>
    /// Class for managing VRoid authentication
    /// </summary>
    public static class Auth
    {
        private static IManualCodeRegistrable _browser;
        public static ISdkConfig SDKConfig;
        public static Client OauthClient;
        public static DefaultApi Api;
        public static Account UserAccount;

        public static void Init()
        {
            var sdkConfigJson = Resources.Load<TextAsset>("credential.json");
            var configText = sdkConfigJson.text;
            SDKConfig = OauthProvider.CreateSdkConfig(configText);
            var driver = new Pixiv.VroidSdk.Networking.Drivers.HttpClientDriver(SynchronizationContext.Current);
            OauthClient = OauthProvider.CreateOauthClient(SDKConfig, driver);
            Api = new DefaultApi(OauthClient);
            _browser = BrowserProvider.Create(OauthClient, SDKConfig);
        }

        public static void Login(Action callBack = null)
        {
            OauthClient.Login(_browser, account =>
            {
                UserAccount = account;
                callBack?.Invoke();
            }, (_) => { });
        }

        public static void OnRegisterCode(string code)
        {
            _browser.OnRegisterCode(code);
        }
        
        public static void Logout()
        {
            OauthClient.ReleaseAuthorizedAccount();
        }
    }
}

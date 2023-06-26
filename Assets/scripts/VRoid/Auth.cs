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
    public static class Auth
    {
        private static IManualCodeRegistrable _browser;
        public static ISdkConfig SDKConfig;
        private static Client _oauthClient;
        public static DefaultApi Api;
        public static Account UserAccount;

        public static void Init()
        {
            var sdkConfigJson = Resources.Load<TextAsset>("credential.json");
            var configText = sdkConfigJson.text;
            SDKConfig = OauthProvider.CreateSdkConfig(configText);
            var driver = new Pixiv.VroidSdk.Networking.Drivers.HttpClientDriver(SynchronizationContext.Current);
            _oauthClient = OauthProvider.CreateOauthClient(SDKConfig, driver);
            Api = new DefaultApi(_oauthClient);
            _browser = BrowserProvider.Create(_oauthClient, SDKConfig);
        }

        public static void Login(Action callBack = null)
        {
            _oauthClient.Login(_browser, account =>
            {
                UserAccount = account;
                callBack?.Invoke();
            }, (_) => { });
        }

        public static void OnRegisterCode(string code)
        {
            _browser.OnRegisterCode(code);
        }
    }
}

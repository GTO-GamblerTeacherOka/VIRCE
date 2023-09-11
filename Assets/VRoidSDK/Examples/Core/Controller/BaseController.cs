using System;
using Pixiv.VroidSdk.Api.DataModel; // Add DataModel reference
using VRoidSDK.Examples.Core.Renderer;

namespace VRoidSDK.Examples.Core.Controller
{
    public class BaseController
    {
        public void CheckLogin(ApiController api, Action<IRenderer> onResponse, Action<Account> onLoggedIn)
        {
            if (!api.IsAuthorized())
            {
                api.OpenLogin(onResponse);
            }

            api.LoginToVroidHub(onResponse, onLoggedIn);
        }
    }
}

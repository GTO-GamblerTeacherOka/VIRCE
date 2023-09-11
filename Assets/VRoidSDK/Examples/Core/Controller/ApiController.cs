using System;
using Pixiv.VroidSdk;
using Pixiv.VroidSdk.Api;
using Pixiv.VroidSdk.Api.Params;
using Pixiv.VroidSdk.Api.DataModel; // for DataModel reference
using Pixiv.VroidSdk.Browser;
using Pixiv.VroidSdk.Oauth;
using Pixiv.VroidSdk.Networking.Drivers;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.Core.Model;
using System.Collections.Generic;
using UnityEngine;
using VRoid;

namespace VRoidSDK.Examples.Core.Controller
{
    public class ApiController
    {
        private ApiModel _model;
        private Client _oauthClient;
        private DefaultApi _api;
        private IManualCodeRegistrable _browser;
        private bool _isManual;
        private bool _useDeviceFlow;
        private bool _isFirstOpenLogin = true;

        /// <summary>
        /// Controller class to call the VRoidHub API.
        ///
        /// You can't call most APIs without logged in.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="driver"></param>
        /// <param name="useDeviceFlow">
        /// Whether the application use OAuth 2.0 device authorization grant for login.
        /// default is false.
        /// </param>
        public ApiController(ISdkConfig config, IHttpConnectionDriver driver, bool useDeviceFlow = false)
        {
            _oauthClient = Auth.OauthClient;
            _browser = BrowserProvider.Create(_oauthClient, config);
            _api = Auth.Api;
            _model = new ApiModel(_oauthClient.IsAccountFileExist());
            _isManual = config.IsManualLogin;
            _useDeviceFlow = useDeviceFlow;
        }

        public bool IsAuthorized()
        {
            return _model.IsAuthorized();
        }

        public string GetLoggedInUserId()
        {
            return _model.CurrentUser?.user_detail.user.id;
        }

        public Client GetOAuthClient()
        {
            return _oauthClient;
        }

        public IDownloadLicensePublishable GetDownloadLicensePublishable()
        {
            return _api;
        }

        public void OpenLogin(Action<IRenderer> onResponse)
        {
            // open login modal if it was already authorized, even if first time.
            if (!IsAuthorized() && _isFirstOpenLogin)
            {
                _model.Active = false;
                _isFirstOpenLogin = false;
            }
            else
            {
                _model.Active = true;
            }

            onResponse(SelectLoginViewRenderer(_useDeviceFlow, _model));
        }

        public void CloseLogin(Action<IRenderer> onResponse)
        {
            _model.Active = false;
            onResponse(SelectLoginViewRenderer(_useDeviceFlow, _model));
        }

        public void Login(Action<IRenderer> onResponse)
        {
            if (_model.IsAuthorized())
            {
                onResponse(SelectLoginViewRenderer(_useDeviceFlow, _model));
                return;
            }

            LoginToVroidHub(onResponse, (account) => UnityEngine.Debug.Log("User LoggedIn Success."));
        }

        public void Logout(Action<IRenderer> onResponse)
        {
            if (!_model.IsAuthorized())
            {
                onResponse(SelectLoginViewRenderer(_useDeviceFlow, _model));
                return;
            }

            _oauthClient.ReleaseAuthorizedAccount();
            _model.ClearUserInfo();
            onResponse(SelectLoginViewRenderer(_useDeviceFlow, _model));
        }

        public void LoginToVroidHub(Action<IRenderer> onResponse, Action<Account> onLoggedIn, int retryCount = 0)
        {
            if (_model.IsAuthorized())
            {
                GetAccountInfo((account) =>
                {
                    _model.CurrentUser = account;
                    _model.Active = false;
                    onLoggedIn(account);
                }, (error) =>
                {
                    // Get this error code if you could not get access token.
                    // It will open browser to re-authorize.
                    if (error.code == "AUTHORIZED_ERROR" && retryCount == 0)
                    {
                        _model.ClearUserInfo();
                        _oauthClient.ReleaseAuthorizedAccount();

                        LoginToVroidHub(onResponse, onLoggedIn, retryCount + 1);
                        return;
                    }

                    _model.ApiError = error;
                    onResponse(new ApiErrorRenderer(_model));
                });
                return;
            }

            if (_useDeviceFlow)
            {
                // Login with device authorization grant if it is not authorized.
                _oauthClient.LoginWithDeviceFlow(deviceAuthInfo =>
                {
                    _model.Active = true;
                    _model.DeviceAuthorization = deviceAuthInfo;
                    _model.AuthorizationState = ApiModel.State.REQUEST_POOLED;
                    // Show user code and URI.
                    onResponse(new DeviceLoginViewRenderer(_model));
                }, (_) =>
                {
                    // Close login modal.
                    _model.Active = false;
                    _model.AuthorizationState = ApiModel.State.AUTHORIZED;
                    onResponse(new DeviceLoginViewRenderer(_model));
                    GetAccountInfo((account) =>
                    {
                        _model.CurrentUser = account;
                        _model.Active = false;
                        onLoggedIn(account);
                    }, (error) =>
                    {
                        // Get this error code if you could not get access token.
                        // It will open browser to re-authorize.
                        if (error.code == "AUTHORIZED_ERROR" && retryCount == 0)
                        {
                            _model.ClearUserInfo();
                            _oauthClient.ReleaseAuthorizedAccount();

                            LoginToVroidHub(onResponse, onLoggedIn, retryCount + 1);
                            return;
                        }

                        _model.ApiError = error;
                        onResponse(new ApiErrorRenderer(_model));
                    });
                }, (e) =>
                {
                    _model.AuthorizationState = ApiModel.State.CONNECTION_FAILED;
                    onResponse(new DeviceLoginViewRenderer(_model));
                });
                return;
            }

            if (_isManual || !_oauthClient.IsAccountFileExist())
            {
                // open login modal.
                _model.Active = true;
                _model.AuthorizationState = ApiModel.State.AUTHORIZATION_CODE_REQUESTED;
                onResponse(new LoginViewRenderer(_model));
            }


            // Open a browser and enter the code if it is not authorized.
            _oauthClient.Login(_browser, (_) =>
            {
                // Close login modal.
                _model.Active = false;
                _model.AuthorizationState = ApiModel.State.AUTHORIZED;
                onResponse(new LoginViewRenderer(_model));
                GetAccountInfo((account) =>
                {
                    _model.CurrentUser = account;
                    _model.Active = false;
                    onLoggedIn(account);
                }, (error) =>
                {
                    _model.ApiError = error;
                    onResponse(new ApiErrorRenderer(_model));
                });
            }, (e) =>
            {
                _model.AuthorizationState = ApiModel.State.CONNECTION_FAILED;
                onResponse(new LoginViewRenderer(_model));
            });
        }

        public void SendAuthorizationCode(string code)
        {
            _browser?.OnRegisterCode(code);
        }

        public void PostArtworkMediaImages(PostArtworkMediaImagesParams requestParams, Action<ArtworkMedium> onSuccess, Action<float> onProgress, Action<ApiErrorFormat> onError)
        {
            _api.PostArtworkMediaImages(requestParams, onSuccess, onProgress, onError);
        }

        public void PostArtworks(PostArtworksParams requestParams, Action<ArtworkDetail> onSuccess, Action<ApiErrorFormat> onError)
        {
            _api.PostArtworks(requestParams, onSuccess, onError);
        }

        public void GetArtworks(string artworkId, Action<ArtworkDetail> onSuccess, Action<ApiErrorFormat> onError)
        {
            _api.GetArtworks(artworkId, onSuccess, onError);
        }

        public void GetUsersArtworks(User user, int count, Action<List<Artwork>, ApiLinksFormat> onSuccess, Action<ApiErrorFormat> onError)
        {
            _api.GetUsersArtworks(user, count, onSuccess, onError);
        }

        public void GetCharacterModelsProperty(string characterModelId, Action<CharacterModelProperty> onSuccess, Action<ApiErrorFormat> onError)
        {
            _api.GetCharacterModelsProperty(characterModelId, onSuccess, onError);
        }

        public void GetAccountCharacterModels(int count, Action<List<CharacterModel>, ApiLinksFormat> onSuccess, Action<ApiErrorFormat> onError)
        {
            _api.GetAccountCharacterModels(count, onSuccess, onError);
        }

        public void GetHearts(int count, Action<List<CharacterModel>, ApiLinksFormat> onSuccess, Action<ApiErrorFormat> onError)
        {
            _api.GetHearts(count, onSuccess, onError);
        }

        public void GetStaffPicks(int count, Action<List<StaffPicksCharacterModel>, ApiLinksFormat> onSuccess, Action<ApiErrorFormat> onError)
        {
            _api.GetStaffPicks(count, onSuccess, onError);
        }

        private void GetAccountInfo(Action<Account> onGetAccount, Action<ApiErrorFormat> onFailed)
        {
            if (_model.CurrentUser != null)
            {
                onGetAccount(_model.CurrentUser);
                return;
            }

            _api.GetAccount(onGetAccount, onFailed);
        }

        private IRenderer SelectLoginViewRenderer(bool useDeviceFlow, ApiModel model)
        {
            if (useDeviceFlow)
            {
                return new DeviceLoginViewRenderer(model);
            }
            else
            {
                return new LoginViewRenderer(model);
            }
        }
    }
}

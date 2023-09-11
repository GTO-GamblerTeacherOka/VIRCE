using Pixiv.VroidSdk.Api.DataModel; // Add DataModel reference
using VRoidSDK.Examples.Core.View;
using VRoidSDK.Examples.Core.Model;
using VRoidSDK.Examples.Core.Localize;

namespace VRoidSDK.Examples.Core.Renderer
{
    public class DeviceLoginViewRenderer : IRenderer
    {
        private bool _panelActive;
        private ApiModel.State _authorizedState;
        private string _currentUserName;
        private WebImage _currentUserIcon;
        private string _verificationURI;
        private string _userCode;

        public DeviceLoginViewRenderer(ApiModel model)
        {
            _panelActive = model.Active;
            _authorizedState = model.AuthorizationState;
            if (model.CurrentUser != null)
            {
                _currentUserName = model.CurrentUser?.user_detail.user.name;
                _currentUserIcon = model.CurrentUser?.user_detail.user.icon.sq170;
            }
            if (model.DeviceAuthorization != null)
            {
                _verificationURI = model.DeviceAuthorization?.verification_uri;
                _userCode = model.DeviceAuthorization?.user_code.Insert(3, " - ");
            }
        }

        public void Rendering(RootView root)
        {
            root.ApiErrorMessage.Active = false;

            if (_panelActive)
            {
                root.deviceLoginView.Active = true;
            }

            switch (_authorizedState)
            {
                case ApiModel.State.AUTHORIZED:
                    root.deviceLoginView.loginConnectButton.Active = false;
                    root.deviceLoginView.logoutButton.Active = true;
                    root.deviceLoginView.logoutButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewLoginLogout);
                    root.deviceLoginView.loginConnectButton.Active = false;
                    root.deviceLoginView.connectionFailureMessageText.Active = false;
                    root.deviceLoginView.loginFailureMessageText.Active = false;
                    root.deviceLoginView.verificationURIField.Active = false;
                    root.deviceLoginView.userCodeField.Active = false;

                    if (_currentUserName != null)
                    {
                        root.deviceLoginView.accountInfo.Active = true;
                        root.deviceLoginView.accountInfo.Set(_currentUserName, _currentUserIcon);
                    }
                    break;
                case ApiModel.State.NOT_AUTHORIZED:
                    root.deviceLoginView.loginConnectButton.Active = false;
                    root.deviceLoginView.logoutButton.Active = false;
                    root.deviceLoginView.accountInfo.Active = false;
                    root.deviceLoginView.loginConnectButton.Active = true;
                    root.deviceLoginView.loginConnectButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewLoginLoginConnect);
                    root.deviceLoginView.connectionFailureMessageText.Active = false;
                    root.deviceLoginView.loginFailureMessageText.Active = false;
                    root.deviceLoginView.verificationURIField.Active = false;
                    root.deviceLoginView.userCodeField.Active = false;

                    root.loginView.Active = false;
                    break;
                case ApiModel.State.REQUEST_POOLED:
                    root.deviceLoginView.loginConnectButton.Active = false;
                    root.deviceLoginView.logoutButton.Active = false;
                    root.deviceLoginView.accountInfo.Active = false;
                    root.deviceLoginView.loginConnectButton.Active = false;
                    root.deviceLoginView.connectionFailureMessageText.Active = false;
                    root.deviceLoginView.loginFailureMessageText.Active = false;
                    root.deviceLoginView.verificationURIField.Active = true;
                    root.deviceLoginView.verificationURIField.verificationURIDescription.Text = Translator.Lang.Get(ExampleViewKey.ViewLoginVerificationURIDescription);
                    root.deviceLoginView.verificationURIField.verificationURI.Text = _verificationURI;
                    root.deviceLoginView.userCodeField.Active = true;
                    root.deviceLoginView.userCodeField.userCodeDescription.Text = Translator.Lang.Get(ExampleViewKey.ViewLoginUserCodeDescription);
                    root.deviceLoginView.userCodeField.userCode.Text = _userCode;

                    root.loginView.Active = false;
                    break;
                case ApiModel.State.CONNECTION_FAILED:
                    root.deviceLoginView.loginConnectButton.Active = false;
                    root.deviceLoginView.logoutButton.Active = false;
                    root.deviceLoginView.accountInfo.Active = false;
                    root.deviceLoginView.loginConnectButton.Active = true;
                    root.deviceLoginView.loginConnectButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewLoginLoginConnect);
                    root.deviceLoginView.connectionFailureMessageText.Active = true;
                    root.deviceLoginView.connectionFailureMessageText.Text = Translator.Lang.Get(ExampleViewKey.ViewLoginConnectionFailureMessage);
                    root.deviceLoginView.loginFailureMessageText.Active = false;
                    root.deviceLoginView.verificationURIField.Active = false;
                    root.deviceLoginView.userCodeField.Active = false;

                    root.loginView.Active = false;
                    break;
                case ApiModel.State.REQUEST_FAILED:
                    root.deviceLoginView.loginConnectButton.Active = false;
                    root.deviceLoginView.logoutButton.Active = false;
                    root.deviceLoginView.accountInfo.Active = false;
                    root.deviceLoginView.loginConnectButton.Active = true;
                    root.deviceLoginView.loginConnectButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewLoginLoginConnect);
                    root.deviceLoginView.connectionFailureMessageText.Active = false;
                    root.deviceLoginView.loginFailureMessageText.Active = true;
                    root.deviceLoginView.loginFailureMessageText.Text = Translator.Lang.Get(ExampleViewKey.ViewLoginLoginFailureMessage);
                    root.deviceLoginView.verificationURIField.Active = false;
                    root.deviceLoginView.userCodeField.Active = false;

                    root.loginView.Active = false;
                    break;
                default:
                    break;
            }

            if (!_panelActive)
            {
                root.deviceLoginView.Active = false;
            }
        }
    }
}

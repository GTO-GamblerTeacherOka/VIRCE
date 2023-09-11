using Pixiv.VroidSdk.Api.DataModel;
using OauthDataModel = Pixiv.VroidSdk.Oauth.DataModel;


namespace VRoidSDK.Examples.Core.Model
{
    public class ApiModel : ApplicationModel
    {
        public enum State
        {
            AUTHORIZED,
            NOT_AUTHORIZED,
            AUTHORIZATION_CODE_REQUESTED,
            REQUEST_POOLED,
            CONNECTION_FAILED,
            REQUEST_FAILED,
        }

        public State AuthorizationState { get; set; }
        // Breaking: Account is not nullable
        public Account CurrentUser { get; set; }
        public OauthDataModel.DeviceAuthorization DeviceAuthorization { get; set; }

        public ApiModel(bool isLoggedIn)
        {
            AuthorizationState = isLoggedIn ? State.AUTHORIZED : State.NOT_AUTHORIZED;
            CurrentUser = null;
            DeviceAuthorization = null;
        }

        public bool IsAuthorized()
        {
            return AuthorizationState == State.AUTHORIZED;
        }

        public void ClearUserInfo()
        {
            AuthorizationState = State.NOT_AUTHORIZED;
            CurrentUser = null;
        }
    }
}

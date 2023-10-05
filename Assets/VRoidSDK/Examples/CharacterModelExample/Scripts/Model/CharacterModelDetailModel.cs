using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Model;

namespace VRoidSDK.Examples.CharacterModelExample.Model
{
    public class CharacterModelDetailModel : ApplicationModel
    {
        public CharacterModel CharacterModel { get; set; }
        public bool IsLicenseAccepted { get; set; }
    }
}

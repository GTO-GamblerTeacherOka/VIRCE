using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Model;

namespace VRoidSDK.Examples.CharacterModelExample.Model
{
    public class CharacterModelDownloadModel : ApplicationModel
    {
        public float Progress { get; set; }
        public bool ThumbnailLoad { get; set; }
        public bool IsAccepted { get; set; }
        public CharacterModel CharacterModel { get; set; }
    }
}

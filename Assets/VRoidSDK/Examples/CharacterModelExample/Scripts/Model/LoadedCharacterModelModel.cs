using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Model;

namespace VRoidSDK.Examples.CharacterModelExample.Model
{
    public class LoadedCharacterModelModel : ApplicationModel
    {
        public CharacterModel CharacterModel { get; set; }

        public LoadedCharacterModelModel()
        {
            CharacterModel = null;
        }
    }
}

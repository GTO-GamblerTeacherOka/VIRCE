using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Model;

namespace VRoidSDK.Examples.CharacterModelExample.Model
{
    public class CharacterModelPropertyModel : ApplicationModel
    {
        public CharacterModelProperty CharacterModelProperty { get; set; }

        public CharacterModelPropertyModel()
        {
            CharacterModelProperty = null;
        }
    }
}

using System.Collections.Generic;
using VRoidSDK.Examples.Core.Model;
using Pixiv.VroidSdk.Api.DataModel;

namespace VRoidSDK.Examples.CharacterModelExample.Model
{
    public class CharacterModelsModel : ApplicationModel
    {
        public enum Tab
        {
            YOURS = 0,
            LIKE = 1,
            PICKUP = 2,
        }

        public Tab ActiveTab { get; set; }
        public Account CurrentUser { get; set; }
        public List<CharacterModel> CharacterModels { get; set; }
        public bool IsLicenseAccepted { get; set; }
        public ApiLinksFormat Next { get; set; }

        public CharacterModelsModel()
        {
            ActiveTab = Tab.YOURS;
            IsLicenseAccepted = false;
        }

        public void MergeCharacterModels(List<CharacterModel> characterModels)
        {
            CharacterModels.AddRange(characterModels);
        }
    }
}

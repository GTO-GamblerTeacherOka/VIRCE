using System.Collections.Generic;
using UnityEngine;
using VRoidSDK.Examples.Core.View.Parts;
using VRoidSDK.Examples.Core.View;
using Pixiv.VroidSdk.Api.DataModel;

namespace VRoidSDK.Examples.CharacterModelExample.View
{
    public class CharacterModelsView : BaseView
    {
#pragma warning disable 0649
        [SerializeField] private Routes _routes;
#pragma warning restore 0649

        public Tab selectTab;
        public VerticalScrollGroup userCharacterModelsScrollRoot;
        public LoadImage userCharacterModelsUserIcon;
        public Button seeMoreButton;

        public void SetUserIcon(WebImage icon)
        {
            userCharacterModelsUserIcon.Load(icon);
        }

        public void SetCharacterModelThumbnails(List<CharacterModel> characterModels)
        {
            userCharacterModelsScrollRoot.Insert<CharacterModel, CharacterModelThumbnail>(characterModels, (characterModelThumb) =>
            {
                _routes.ShowCharacterModel(characterModelThumb.CharacterModel);
            });
        }
    }
}

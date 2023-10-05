using System.Collections.Generic;
using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.Core.View;
using VRoidSDK.Examples.CharacterModelExample.Model;
using VRoidSDK.Examples.CharacterModelExample.View;

namespace VRoidSDK.Examples.CharacterModelExample.Renderer
{
    public class CharacterModelsRenderer : IRenderer
    {
        private Account _currentUser;
        private bool _active;
        private CharacterModelsModel.Tab _activeTab;
        private List<CharacterModel> _characterModels;
        private ApiLinksFormat _next;

        public CharacterModelsRenderer(CharacterModelsModel model)
        {
            _active = model.Active;
            _currentUser = model.CurrentUser;
            _characterModels = model.CharacterModels;
            _activeTab = model.ActiveTab;
            _next = model.Next;
        }

        public void Rendering(RootView root)
        {
            root.ApiErrorMessage.Active = false;
            var characterModelRoot = (CharacterModelRootView)root;
            characterModelRoot.characterModelsView.Active = _active;

            if (_active)
            {
                characterModelRoot.characterModelsView.selectTab.ActiveIndex = (int)_activeTab;
                characterModelRoot.characterModelsView.SetUserIcon(_currentUser?.user_detail.user.icon.sq170);
                characterModelRoot.characterModelsView.SetCharacterModelThumbnails(_characterModels);

                characterModelRoot.characterModelsView.seeMoreButton.Active = _next.next != null;
            }
        }
    }
}

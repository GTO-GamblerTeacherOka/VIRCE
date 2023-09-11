using System.Collections.Generic;
using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.Core.View;
using VRoidSDK.Examples.Core.Localize;
using VRoidSDK.Examples.CharacterModelExample.Model;
using VRoidSDK.Examples.CharacterModelExample.View;

namespace VRoidSDK.Examples.CharacterModelExample.Renderer
{
    public class CharacterModelDownloadRenderer : IRenderer
    {
        private bool _isActive;
        private bool _isAccept;
        private bool _isThumbnailLoad;
        private string _characterName;
        private string _characterModelName;
        private string _characterPublisherName;
        private WebImage _portraitImage;
        private float _downloadProgress;

        public CharacterModelDownloadRenderer(CharacterModelDownloadModel model)
        {
            _isActive = model.Active;
            _downloadProgress = model.Progress;
            _isThumbnailLoad = model.ThumbnailLoad;
            _isAccept = model.IsAccepted;
            if (model.CharacterModel != null)
            {
                _characterName = model.CharacterModel?.character.name;
                _characterModelName = model.CharacterModel?.name;
                _characterPublisherName = model.CharacterModel?.character.user.name;
                _portraitImage = model.CharacterModel?.portrait_image.sq150;
            }
        }

        public void Rendering(RootView root)
        {
            var characterModelRoot = (CharacterModelRootView)root;
            var downloadView = characterModelRoot.characterModelDownloadView;
            characterModelRoot.overlay.Active = _isActive;
            downloadView.Active = _isActive;

            if (_isThumbnailLoad)
            {
                downloadView.characterModelIcon.Load(_portraitImage);
                downloadView.characterName.Text = _characterName;
                downloadView.characterModelName.Text = _characterModelName;
                downloadView.characterModelPublisherName.Text = _characterPublisherName;
            }

            if (!_isAccept)
            {
                downloadView.buttonGroup.Active = true;
                downloadView.buttonGroup.acceptButton.Active = true;
                downloadView.buttonGroup.acceptButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailModelUse);
                downloadView.buttonGroup.cancelButton.Active = true;
                downloadView.buttonGroup.cancelButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailModelUseCancel);
                downloadView.downloadProgress.Active = false;
                downloadView.loadingMessage.Active = false;
            }
            else if (_downloadProgress >= 1.0f)
            {
                downloadView.buttonGroup.Active = false;
                downloadView.downloadProgress.Active = false;
                downloadView.loadingMessage.Active = true;
                downloadView.loadingMessage.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailLoading);
            }
            else
            {
                downloadView.buttonGroup.Active = false;
                downloadView.downloadProgress.Active = true;
                downloadView.downloadProgress.Value = _downloadProgress;
                downloadView.loadingMessage.Active = false;
            }
        }
    }
}

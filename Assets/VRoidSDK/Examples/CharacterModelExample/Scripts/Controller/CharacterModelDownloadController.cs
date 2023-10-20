using System;
using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Controller;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.CharacterModelExample.Model;
using VRoidSDK.Examples.CharacterModelExample.Renderer;

namespace VRoidSDK.Examples.CharacterModelExample.Controller
{
    public class CharacterModelDownloadController : BaseController
    {
        private CharacterModelDownloadModel _model;

        public CharacterModelDownloadController()
        {
            _model = new CharacterModelDownloadModel();
        }

        public void Open(CharacterModel characterModel, Action<IRenderer> onResponse)
        {
            _model.CharacterModel = characterModel;
            _model.Active = true;
            _model.ThumbnailLoad = true;
            _model.IsAccepted = true;
            onResponse(new CharacterModelDownloadRenderer(_model));
        }

        public void OpenWithoutAccept(CharacterModel characterModel, Action<IRenderer> onResponse)
        {
            _model.CharacterModel = characterModel;
            _model.Active = true;
            _model.ThumbnailLoad = true;
            _model.IsAccepted = false;
            onResponse(new CharacterModelDownloadRenderer(_model));
        }

        public void SeekTo(float progress, Action<IRenderer> onResponse)
        {
            _model.Progress = progress;
            _model.ThumbnailLoad = false;
            onResponse(new CharacterModelDownloadRenderer(_model));
        }

        public void Close(Action<IRenderer> onResponse)
        {
            _model.CharacterModel = null;
            _model.Active = false;
            _model.ThumbnailLoad = false;
            onResponse(new CharacterModelDownloadRenderer(_model));
        }
    }
}

using System;
using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Controller;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.CharacterModelExample.Model;
using VRoidSDK.Examples.CharacterModelExample.Renderer;

namespace VRoidSDK.Examples.CharacterModelExample.Controller
{
    public class CharacterModelPropertyController : BaseController
    {
        private ApiController _api;
        private CharacterModelPropertyModel _model;

        public CharacterModelPropertyController(ApiController api)
        {
            _model = new CharacterModelPropertyModel();
            _api = api;
        }

        public void GetCharacterModelProperty(CharacterModel characterModel, Action<IRenderer> onResponse)
        {
            CheckLogin(_api, onResponse, (_) =>
            {
                _api.GetCharacterModelsProperty(characterModel.id,
                    (property) => _model.CharacterModelProperty = property,
                    (error) =>
                    {
                        _model.ApiError = error;
                        onResponse(new ApiErrorRenderer(_model));
                    });
            });
        }

        public void ShowCharacterModelProperty(Action<IRenderer> onResponse)
        {
            _model.Active = true;
            onResponse(new CharacterModelPropertyRenderer(_model));
        }

        public void HideCharacterModelProperty(Action<IRenderer> onResponse)
        {
            _model.Active = false;
            onResponse(new CharacterModelPropertyRenderer(_model));
        }
    }
}

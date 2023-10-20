using System;
using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Controller;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.CharacterModelExample.Model;
using VRoidSDK.Examples.CharacterModelExample.Renderer;
using UnityEngine;

namespace VRoidSDK.Examples.CharacterModelExample.Controller
{
    public class LoadedCharacterModelController : BaseController
    {
        private LoadedCharacterModelModel _model;
        private CharacterModelPropertyController _propertyController;
        private CharacterModelExampleEventHandler _eventHandler;

        public LoadedCharacterModelController(CharacterModelPropertyController propertyController, CharacterModelExampleEventHandler eventHandler)
        {
            _model = new LoadedCharacterModelModel();
            _propertyController = propertyController;
            _eventHandler = eventHandler;
        }

        public void SetLoadedModel(CharacterModel characterModel, GameObject go, Action<IRenderer> onResponse)
        {
            _model.CharacterModel = characterModel;
            _propertyController.GetCharacterModelProperty(characterModel, onResponse);
            if (_eventHandler != null)
            {
                _eventHandler.OnModelLoaded(characterModel.id, go);
            }
        }

        public void ShowLoadedCharacterModel(Action<IRenderer> onResponse)
        {
            _model.Active = true;
            onResponse(new CharacterModelDetailRenderer(_model));
        }
    }
}

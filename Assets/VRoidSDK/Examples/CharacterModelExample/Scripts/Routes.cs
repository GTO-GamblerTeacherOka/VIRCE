using System;
using System.Runtime.Serialization;
using System.Threading;
using Pixiv.VroidSdk;
using Pixiv.VroidSdk.Api.DataModel;
using Pixiv.VroidSdk.Networking.Drivers;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.Core.Controller;
using VRoidSDK.Examples.Core.Localize;
using VRoidSDK.Examples.CharacterModelExample.View;
using VRoidSDK.Examples.CharacterModelExample.Controller;
using VRoidSDK.Examples.CharacterModelExample.Model;
using UnityEngine;
using VRoid;

namespace VRoidSDK.Examples.CharacterModelExample
{
    public class Routes : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private CharacterModelRootView _rootView;
        [SerializeField] private string _appPassword;
        [SerializeField] private bool _useDeviceFlow;
        [SerializeField] private CharacterModelExampleEventHandler _eventHandler;
#pragma warning restore 0649

        private ApiController _apiController;
        private CharacterModelsController _characterModelsController;
        private CharacterModelDetailController _characterModelDetailController;
        private CharacterModelDownloadController _characterModelDownloadController;
        private LoadedCharacterModelController _loadedCharacterModelController;
        private CharacterModelPropertyController _characterModelPropertyController;

        private void Start()
        {
            Auth.Init();
            var config = Auth.SDKConfig;
            var driver = Auth.Driver;
            _apiController = new ApiController(config, driver, _useDeviceFlow);
            _characterModelsController = new CharacterModelsController(_apiController);
            _characterModelDownloadController = new CharacterModelDownloadController();
            _characterModelPropertyController = new CharacterModelPropertyController(_apiController);
            _loadedCharacterModelController = new LoadedCharacterModelController(_characterModelPropertyController, _eventHandler);
            _characterModelDetailController = new CharacterModelDetailController(_apiController, _characterModelsController,
                _characterModelDownloadController, _loadedCharacterModelController);
            ModelLoader.Initialize(config, _apiController.GetDownloadLicensePublishable(), _appPassword);
        }

        private ISdkConfig LoadConfigFromTextAsset()
        {
            var asset = Resources.Load<TextAsset>("credential.json");
            if (asset == null)
            {
                throw new NullReferenceException("You have to place the credential.json.bytes in any of the Resources folders");
            }

            try
            {
                return OauthProvider.CreateSdkConfig(asset.text);
            }
            catch (SerializationException)
            {
                Debug.LogError($"Could not parse textAsset: {asset.text}");
                throw;
            }
        }

        public void ShowCharacterModels()
        {
            _characterModelsController.ShowCharacterModels(Rendering);
        }

        public void HideCharacterModels()
        {
            _characterModelsController.HideCharacterModels(Rendering);
        }

        public void ShowCharacterModel(CharacterModel model)
        {
            _characterModelDetailController.ShowCharacterModel(model, Rendering);
        }

        public void HideCharacterModel()
        {
            _characterModelDetailController.HideCharacterModel(Rendering);
        }

        public void SeeMore()
        {
            _characterModelsController.ShowNextCharacterModels(Rendering);
        }

        public void CheckAccept(UnityEngine.UI.Toggle toggle)
        {
            var result = toggle.isOn;
            _characterModelDetailController.CheckAccept(result, Rendering);
        }

        public void UseCharacterModel()
        {
            _characterModelDetailController.UseCharacterModel(Rendering);
        }

        public void ChangeTab(int tab)
        {
            _characterModelsController.ChangeTab((CharacterModelsModel.Tab)tab, Rendering);
        }

        public void SendAuthorizeCode(UnityEngine.UI.InputField code)
        {
            var text = code.text;
            _apiController.SendAuthorizationCode(text);
        }

        public void ShowLoginPanel()
        {
            _apiController.OpenLogin(Rendering);
        }

        public void CloseLoginPanel()
        {
            _apiController.CloseLogin(Rendering);
        }

        public void Logout()
        {
            _apiController.Logout(Rendering);
            HideCharacterModels();
        }

        public void Login()
        {
            _apiController.Login(Rendering);
        }

        public void LocalizeChanged(int locale)
        {
            var translateLocale = (Translator.Locales)locale;
            Translator.ChangeTo(translateLocale);
            if (_eventHandler)
            {
                _eventHandler.OnLangChanged(translateLocale);
            }
        }

        public void HideDownloadModel()
        {
            _characterModelDownloadController.Close(Rendering);
        }

        public void ShowLoadedCharacterModel()
        {
            _loadedCharacterModelController.ShowLoadedCharacterModel(Rendering);
        }

        public void ShowCharacterModelProperty()
        {
            _characterModelPropertyController.ShowCharacterModelProperty(Rendering);
        }

        public void HideCharacterModelProperty()
        {
            _characterModelPropertyController.HideCharacterModelProperty(Rendering);
        }

        private void Rendering(IRenderer renderer)
        {
            renderer.Rendering(_rootView);
        }
    }
}

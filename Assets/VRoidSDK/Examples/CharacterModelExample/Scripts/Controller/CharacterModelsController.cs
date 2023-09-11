using System;
using System.Collections.Generic;
using System.Linq;
using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.Controller;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.CharacterModelExample.Model;
using VRoidSDK.Examples.CharacterModelExample.Renderer;

namespace VRoidSDK.Examples.CharacterModelExample.Controller
{
    public class CharacterModelsController : BaseController
    {
        private ApiController _api;
        private CharacterModelsModel _model;

        public CharacterModelsController(ApiController api)
        {
            _api = api;
            _model = new CharacterModelsModel();
        }

        public void ShowCharacterModels(Action<IRenderer> onResponse)
        {
            CheckLogin(_api, onResponse, (account) =>
            {
                _model.CurrentUser = account;
                TabRequest(_model.ActiveTab, onResponse);
            });
        }

        public void HideCharacterModels(Action<IRenderer> onResponse)
        {
            _model.Active = false;
            onResponse(new CharacterModelsRenderer(_model));
        }

        public void ChangeTab(CharacterModelsModel.Tab tab, Action<IRenderer> onResponse)
        {
            CheckLogin(_api, onResponse, (_) =>
            {
                _model.ActiveTab = tab;
                TabRequest(tab, onResponse);
            });
        }

        public void ShowNextCharacterModels(Action<IRenderer> onResponse)
        {
            CheckLogin(_api, onResponse, (_) =>
            {
                if (_model.ActiveTab == CharacterModelsModel.Tab.PICKUP)
                {
                    _model.Next.next?.RequestLink<List<StaffPicksCharacterModel>>((staffPicksCharacterModel, link) =>
                    {
                        var characterModels = staffPicksCharacterModel.Select((x) => x.character_model);
                        _model.MergeCharacterModels(characterModels.ToList());
                        _model.Next = link;
                        onResponse(new CharacterModelsRenderer(_model));
                    }, GetOnErrorCallback(onResponse));
                    return;
                }

                _model.Next.next?.RequestLink<List<CharacterModel>>((characterModels, link) =>
                {
                    _model.MergeCharacterModels(characterModels);
                    _model.Next = link;
                    onResponse(new CharacterModelsRenderer(_model));
                }, GetOnErrorCallback(onResponse));
            });
        }

        private void TabRequest(CharacterModelsModel.Tab tab, Action<IRenderer> onResponse)
        {
            switch (tab)
            {
                case CharacterModelsModel.Tab.YOURS:
                    _api.GetAccountCharacterModels(10, GetCharacterModelsCallback(onResponse), GetOnErrorCallback(onResponse));
                    break;
                case CharacterModelsModel.Tab.LIKE:
                    _api.GetHearts(10, GetCharacterModelsCallback(onResponse), GetOnErrorCallback(onResponse));
                    break;
                case CharacterModelsModel.Tab.PICKUP:
                    _api.GetStaffPicks(10, (staffPicksCharacterModel, link) =>
                    {
                        var characterModels = staffPicksCharacterModel.Select((x) => x.character_model);
                        GetCharacterModelsCallback(onResponse)(characterModels.ToList(), link);
                    }, GetOnErrorCallback(onResponse));
                    break;
                default:
                    break;
            }
        }

        private Action<List<CharacterModel>, ApiLinksFormat> GetCharacterModelsCallback(Action<IRenderer> onResponse)
        {
            return (characterModels, link) =>
            {
                _model.Next = link;
                _model.Active = true;
                _model.CharacterModels = characterModels;
                onResponse(new CharacterModelsRenderer(_model));
            };
        }

        private Action<ApiErrorFormat> GetOnErrorCallback(Action<IRenderer> onResponse)
        {
            return (error) =>
            {
                _model.ApiError = error;
                onResponse(new ApiErrorRenderer(_model));
            };
        }
    }
}

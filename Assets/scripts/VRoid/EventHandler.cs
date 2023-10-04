using System;
using System.Linq;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRoidSDK.Examples.CharacterModelExample;
using VRoidSDK.Examples.Core.Localize;

namespace VRoid
{
    public class EventHandler : CharacterModelExampleEventHandler
    {
        [SerializeField] private Text selectModelText;
        [SerializeField] private Text licenseCheckText;
        [SerializeField] private Text modelPropertyText;

        public override void OnModelLoaded(string modelId, GameObject go)
        {
            DeleteAllChildren();
            go.transform.parent = transform;
            GameSetting.SetModelId(modelId);
            Auth.MultiplayApi.PostDownloadLicenses(modelId, license =>
            {
                GameSetting.SetModelPublishId(license.id);
                SceneManager.LoadScene("main");
                GameSetting.SetRoomId(1);
                GameSetting.SetUserId(1);
            }, _ => { });
        }

        public override void OnLangChanged(Translator.Locales locale)
        {
            switch (locale)
            {
                case Translator.Locales.JA:
                    selectModelText.text = "モデル選択";
                    licenseCheckText.text = "ライセンス";
                    modelPropertyText.text = "モデル情報";
                    break;
                case Translator.Locales.EN:
                    selectModelText.text = "Select Model";
                    licenseCheckText.text = "License";
                    modelPropertyText.text = "Model Property";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(locale), locale, null);
            }
        }

        private void DeleteAllChildren()
        {
            var transformList = gameObject.transform.Cast<Transform>().ToList();
            gameObject.transform.DetachChildren();
            foreach (var child in transformList) Destroy(child.gameObject);
        }
    }
}
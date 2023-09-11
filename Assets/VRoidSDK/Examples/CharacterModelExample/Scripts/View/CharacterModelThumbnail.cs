using UnityEngine;
using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.View.Parts;

namespace VRoidSDK.Examples.CharacterModelExample.View
{
    public class CharacterModelThumbnail : VerticalScrollItem<CharacterModel>
    {
#pragma warning disable 0649
        [SerializeField] private LoadImage _image;
#pragma warning restore 0649

        public CharacterModel CharacterModel { get; private set; }

        public override void Init(CharacterModel baseData)
        {
            CharacterModel = baseData;
            _image.Load(baseData.portrait_image.sq150);
        }
    }
}

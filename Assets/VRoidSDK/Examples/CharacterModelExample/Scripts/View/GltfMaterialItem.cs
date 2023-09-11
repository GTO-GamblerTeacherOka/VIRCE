using System.Text;
using VRoidSDK.Examples.Core.View.Parts;
using Pixiv.VroidSdk.Api.DataModel;
using Pixiv.VroidSdk.Decorator;
using UnityEngine;

namespace VRoidSDK.Examples.CharacterModelExample.View
{
    public class GltfMaterialItem : VerticalScrollItem<GltfMaterial>
    {
#pragma warning disable 0649
        [SerializeField] private Message _message;
#pragma warning restore 0649

        public override void Init(GltfMaterial baseData)
        {
            var decorator = new TextDecorator(baseData.name);
            var stringBuilder = new StringBuilder(decorator.Color("#B1CC29").Bold().Text);

            if (baseData.extensions != null)
            {
                stringBuilder.Append("\n");
                if (baseData.extensions.KHR_materials_unlit != null)
                {
                    stringBuilder.AppendLine("\t KHR_materials_unlit：{}");
                }
            }

            _message.Text = stringBuilder.ToString();
        }
    }
}

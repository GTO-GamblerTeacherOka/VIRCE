using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.View;
using VRoidSDK.Examples.Core.View.Parts;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.CharacterModelExample.Model;
using VRoidSDK.Examples.CharacterModelExample.View;
using VRoidSDK.Examples.Core.Localize;

namespace VRoidSDK.Examples.CharacterModelExample.Renderer
{
    public class CharacterModelPropertyRenderer : IRenderer
    {
        private bool _isActive;
        private CharacterModelProperty _property;

        public CharacterModelPropertyRenderer(CharacterModelPropertyModel model)
        {
            _isActive = model.Active;
            _property = model.CharacterModelProperty;
        }

        public void Rendering(RootView root)
        {
            var characterModelRootView = (CharacterModelRootView)root;
            characterModelRootView.overlay.Active = _isActive;
            characterModelRootView.characterModelPropertyView.Active = _isActive;

            if (_isActive == false) return;

            characterModelRootView.characterModelPropertyView.headerTitle.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyTitle);
            characterModelRootView.characterModelPropertyView.modelId.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyModelId) + "：" + _property.id;
            characterModelRootView.characterModelPropertyView.specVersion.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertySpecVersion) + "：" + _property.spec_version;
            characterModelRootView.characterModelPropertyView.exporterVersion.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyExporterVersion) + "：" + _property.exporter_version;
            characterModelRootView.characterModelPropertyView.triangleCount.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyTriangleCount) + "：" + _property.triangle_count;
            characterModelRootView.characterModelPropertyView.meshCount.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyMeshCount) + "：" + _property.mesh_count;
            characterModelRootView.characterModelPropertyView.meshPrimitiveCount.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyMeshPrimitiveCount) + "：" + _property.mesh_primitive_count;
            characterModelRootView.characterModelPropertyView.meshPrimitiveMorphCount.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyMeshPrimitiveMorphCount) + "：" + _property.mesh_primitive_morph_count;
            characterModelRootView.characterModelPropertyView.textureCount.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyTextureCount) + "：" + _property.texture_count;
            characterModelRootView.characterModelPropertyView.jointCount.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyJointCount) + "：" + _property.joint_count;
            characterModelRootView.characterModelPropertyView.materialCount.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyMaterialCount) + "：" + _property.material_count;
            characterModelRootView.characterModelPropertyView.closeButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelPropertyCloseButton);

            characterModelRootView.characterModelPropertyView.materialDetails.Insert<GltfMaterial, GltfMaterialItem>(
                _property.character_model_version_material.materials, (_) => { });
        }
    }
}

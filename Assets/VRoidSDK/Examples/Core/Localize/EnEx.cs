using System.Collections.Generic;
using Pixiv.VroidSdk.Localize;

namespace VRoidSDK.Examples.Core.Localize
{
    public class EnEx : En
    {
        private static readonly Dictionary<string, string> s_localeDictionary = new Dictionary<string, string>()
        {
            { ExampleViewKey.ViewLoginAuthorizationCode, "Authorization code" },
            { ExampleViewKey.ViewLoginLoginConnect, "Connect to VRoid Hub" },
            { ExampleViewKey.ViewLoginLogin, "Login" },
            { ExampleViewKey.ViewLoginLogout, "Logout" },
            { ExampleViewKey.ViewLoginConnectionFailureMessage, "Connection Failed" },
            { ExampleViewKey.ViewLoginLoginFailureMessage, "Login Failed" },
            { ExampleViewKey.ViewLoginVerificationURIDescription, "Access the URL below on a smartphone or PC." },
            { ExampleViewKey.ViewLoginUserCodeDescription, "Log in to VRoid Hub, and enter the code below." },
            { ExampleViewKey.ViewCharacterModelDetailAcceptLicense, "I agree to use the model according to the conditions of use" },
            { ExampleViewKey.ViewCharacterModelDetailModelUse, "Download" },
            { ExampleViewKey.ViewCharacterModelDetailModelUseCancel, "CANCEL" },
            { ExampleViewKey.ViewCharacterModelDetailLoading, "Loading now" },
            { ExampleViewKey.ViewCharacterModelPropertyTitle, "Model Property" },
            { ExampleViewKey.ViewCharacterModelPropertyModelId, "Model Id" },
            { ExampleViewKey.ViewCharacterModelPropertySpecVersion, "Spec Version" },
            { ExampleViewKey.ViewCharacterModelPropertyExporterVersion, "Exporter Version" },
            { ExampleViewKey.ViewCharacterModelPropertyTriangleCount, "Triangle Count" },
            { ExampleViewKey.ViewCharacterModelPropertyMeshCount, "Mesh Count" },
            { ExampleViewKey.ViewCharacterModelPropertyMeshPrimitiveCount, "SubMesh Count" },
            { ExampleViewKey.ViewCharacterModelPropertyMeshPrimitiveMorphCount, "Morph Count" },
            { ExampleViewKey.ViewCharacterModelPropertyTextureCount, "Texture Count" },
            { ExampleViewKey.ViewCharacterModelPropertyJointCount, "Joint Count" },
            { ExampleViewKey.ViewCharacterModelPropertyMaterialCount, "Material Count" },
            { ExampleViewKey.ViewCharacterModelPropertyCloseButton, "Close" },
            { ExampleViewKey.ViewArtworkDetailCloseButton, "Close" },
            { ExampleViewKey.ViewArtworkCreateTitle, "Upload media" },
            { ExampleViewKey.ViewArtworkCreateIsArchived, "Post media as archived. To be public, uncheck Is Artchived in Routes." },
            { ExampleViewKey.ViewArtworkCreateCaption, "Caption" },
            { ExampleViewKey.ViewArtworkCreateAgeLimit, "Age limit" },
            { ExampleViewKey.ViewArtworkCreateAgeLimitAll, "All ages" },
            { ExampleViewKey.ViewArtworkCreateAgeLimitR15, "R-15" },
            { ExampleViewKey.ViewArtworkCreateAgeLimitR18, "R-18" },
            { ExampleViewKey.ViewArtworkCreateUpload, "Submit" },
            { ExampleViewKey.ViewArtworkCreateCancel, "Cancel" },
        };

        public override string Get(string key)
        {
            if (s_localeDictionary.ContainsKey(key))
            {
                return s_localeDictionary[key];
            }

            return base.Get(key);
        }
    }
}

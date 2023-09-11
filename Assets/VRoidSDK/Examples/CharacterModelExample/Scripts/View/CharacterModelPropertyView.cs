using VRoidSDK.Examples.Core.View;
using VRoidSDK.Examples.Core.View.Parts;

namespace VRoidSDK.Examples.CharacterModelExample.View
{
    public class CharacterModelPropertyView : BaseView
    {
        public Message headerTitle;
        public Message modelId;
        public Message specVersion;
        public Message exporterVersion;
        public Message triangleCount;
        public Message meshCount;
        public Message meshPrimitiveCount;
        public Message meshPrimitiveMorphCount;
        public Message textureCount;
        public Message jointCount;
        public Message materialCount;
        public VerticalScrollGroup materialDetails;
        public Button closeButton;
    }
}

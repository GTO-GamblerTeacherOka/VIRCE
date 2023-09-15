using VRoidSDK.Examples.Core.View;
using VRoidSDK.Examples.Core.View.Parts;

namespace VRoidSDK.Examples.CharacterModelExample.View
{
    public class CharacterModelDetailView : BaseView
    {
        public LoadImage characterModelIcon;
        public Message characterName;
        public Message characterModelName;
        public Message characterModelPublisherName;
        public Message modelUseConditions;
        public Message vrmFormat;
        public Message canUseAvatar;
        public Message canUseViolence;
        public Message canUseSexuality;
        public Message canUseCorporateCommercial;
        public Message canUsePersonalCommercial;
        public Message canModify;
        public Message canRedistribute;
        public Message showCredit;
        public Button acceptButton;
        public Button retryButton;
        public Button cancelButton;
        public ToggleBox isAccepted;
    }
}

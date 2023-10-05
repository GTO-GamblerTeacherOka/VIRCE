using VRoidSDK.Examples.Core.View;
using VRoidSDK.Examples.Core.View.Parts;

namespace VRoidSDK.Examples.CharacterModelExample.View
{
    public class CharacterModelDownloadView : BaseView
    {
        public LoadImage characterModelIcon;
        public Message characterName;
        public Message characterModelName;
        public Message characterModelPublisherName;
        public ProgressBar downloadProgress;
        public Message loadingMessage;
        public ButtonGroup buttonGroup;
    }
}

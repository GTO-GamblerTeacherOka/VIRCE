using System;

namespace VRoidSDK.Examples.Core.View.Parts
{
    public abstract class VerticalScrollItem<T> : BaseView
    {
        public Action<VerticalScrollItem<T>> OnSelect { get; set; }

        public abstract void Init(T baseData);
        public void OnClickVerticalItem()
        {
            if (OnSelect != null)
            {
                OnSelect(this);
            }
        }
    }
}

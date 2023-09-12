using UnityEngine;
using System.Collections.Generic;

namespace VRoidSDK.Examples.Core.View
{
    public abstract class BaseView : MonoBehaviour
    {
        public bool Active
        {
            get
            {
                return this.gameObject.activeSelf;
            }
            set
            {
                this.gameObject.SetActive(value);
            }
        }

        public void DeleteAllChildren()
        {
            var transformList = new List<Transform>();
            foreach (Transform child in this.gameObject.transform)
            {
                transformList.Add(child);
            }
            this.gameObject.transform.DetachChildren();
            foreach (Transform child in transformList)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public T AddChild<T>(GameObject go)
            where T : UnityEngine.Component
        {
            var instance = GameObject.Instantiate(
                go,
                this.transform.position,
                this.transform.rotation
            );
            instance.transform.SetParent(this.gameObject.transform, false);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localEulerAngles = Vector3.zero;
            instance.transform.localScale = Vector3.one;

            return instance.GetComponent<T>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButtonActor : MonoBehaviour
    {
        protected UniversalEventManager EventManager => GlobalManager.Instance.Get<UniversalEventManager>();

        private Button button;
        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}

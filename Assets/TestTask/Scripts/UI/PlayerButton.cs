using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class PlayerButton : UIButtonActor
    {
        protected override void OnClick() => EventManager.Get<UIEvents>().PlayerButtonPressed?.Invoke();
    }
}

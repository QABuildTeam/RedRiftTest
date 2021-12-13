using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class CardEvents : IEventProvider
    {
        public GEvent<Card, Action<int>, Action<int>, Action<int>> AddCard; // card, attack action, hp action, mp action
        public GEvent<Card> RemoveCard;
        public GEvent<Card> DetachCard;
        public GEvent<Card> ReattachCard;
        public GEvent<Transform, Vector3> BeginDrag;
        public GEvent<Transform, Vector3> Drag;
        public GEvent<Transform, Vector3> EndDrag;
    }
}

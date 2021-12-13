using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    [CreateAssetMenu(fileName = nameof(DeckArcSettings), menuName = "Test Task/Deck Arc Settings", order = 101)]
    public class DeckArcSettings : SettingsTemplate
    {
        [Tooltip("Arc angle value in degrees")]
        public float deckArcAngle;
        public float arcRadius;
        [Tooltip("Card rotation speed, in degrees per second")]
        public float rotationSpeed;
        public float cardReturnSpeed;
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TestTask
{
    [CreateAssetMenu(fileName = nameof(CardSettings), menuName = "Test Task/Card Settings", order = 102)]
    public class CardSettings : SettingsTemplate
    {
        [Serializable]
        public class CardData
        {
            public string title;
            public string description;
            public int attack;
            public int hp;
            public int mp;
        }

        public List<CardData> cardData;

        public float statAnimationSpeed;
        public float glowIntensity;
    }
}

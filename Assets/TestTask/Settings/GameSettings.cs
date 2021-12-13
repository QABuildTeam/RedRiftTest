using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Test Task/Game Settings", order = 103)]
    public class GameSettings : SettingsTemplate
    {
        public int minCardsInDeck;
        public int maxCardsInDeck;
        public int minPropertyValue;
        public int maxPropertyValue;
        public int lowHPValue;
    }
}

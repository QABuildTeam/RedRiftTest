using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public static class CardDataConvert
    {
        public static Card Convert(this CardSettings.CardData data)
        {
            return new Card
            {
                title = data.title,
                description = data.description,
                attack = data.attack,
                hp = data.hp,
                mp = data.mp
            };
        }
    }
}

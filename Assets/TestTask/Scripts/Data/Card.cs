using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class Card
    {
        public VisualValue<string> title;
        public VisualValue<string> description;
        public VisualValue<int> hp;
        public VisualValue<int> mp;
        public VisualValue<int> attack;

        public override string ToString()
        {
            return $"title:{title},description:{description},attack:{attack},hp:{hp},mp:{mp}";
        }
    }
}

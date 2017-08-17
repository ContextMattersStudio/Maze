using UnityEngine;
using System.Collections;

namespace CardboardGestures.Conditions
{
    public class Condition_True : AbstractCondition
    {
        public override bool satisfied()
        {
            return true;
        }

    }
}
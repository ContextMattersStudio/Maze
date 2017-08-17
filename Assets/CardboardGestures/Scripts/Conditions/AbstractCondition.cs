using UnityEngine;
using System.Collections;

namespace CardboardGestures.Conditions
{
    public abstract class AbstractCondition : MonoBehaviour
    {
        public abstract bool satisfied();
    }
}
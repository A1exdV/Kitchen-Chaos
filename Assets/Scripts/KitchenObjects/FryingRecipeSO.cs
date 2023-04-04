using UnityEngine;

namespace KitchenObjects
{
    [CreateAssetMenu()]
    public class FryingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float fryingTimerMax;
    }
}

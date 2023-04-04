using UnityEngine;

namespace KitchenObjects
{
    [CreateAssetMenu()]
    public class CuttingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
    }
}

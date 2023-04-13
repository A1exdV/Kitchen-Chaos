using System.Collections.Generic;
using UnityEngine;

namespace KitchenObjects
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        public string recipeName;
        public List<KitchenObjectSO> kitchenObjectSOList;
    }
}

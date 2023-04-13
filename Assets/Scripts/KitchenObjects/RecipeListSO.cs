using System.Collections.Generic;
using UnityEngine;

namespace KitchenObjects
{
    [CreateAssetMenu()]
    public class RecipeListSO : ScriptableObject
    {
        public List<RecipeSO> recipeSOList;
    }
}

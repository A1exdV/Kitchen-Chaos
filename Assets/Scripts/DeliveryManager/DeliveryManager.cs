using System;
using System.Collections;
using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public static event EventHandler OnRecipeSuccess;
    public static event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO recipeListSO;

    public static DeliveryManager Instance { get; private set; }

    private List<RecipeSO> _waitingRecipeList;

    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMin = 4f;
    private float _spawnRecipeTimerMax = 8f;
    private int _waitingRecipesMax = 6;

    private void Awake()
    {
        _waitingRecipeList = new List<RecipeSO>();
        Instance = this;
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;

        if (_spawnRecipeTimer <= 0)
        {
            _spawnRecipeTimer = Random.Range(_spawnRecipeTimerMin, _spawnRecipeTimerMax);

            if (_waitingRecipeList.Count < _waitingRecipesMax)
            {
                var waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                _waitingRecipeList.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject, GameObject sender)
    {
        for (var i = 0; i < _waitingRecipeList.Count; i++)
        {
            var waitingRecipeSO = _waitingRecipeList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients

                var plateContentsMatchesRecipe = true;

                foreach (var recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling throgh all ingredients in the recipe

                    var ingredientFound = false;

                    foreach (var plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling throgh all ingredients in the plate

                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredient matcha!

                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        // This Recipe ingridient was not found on the Plate

                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    //Player delivered the correct plate!

                    _waitingRecipeList.RemoveAt(i);
                    print("done!");
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(sender, EventArgs.Empty);
                    return;
                }
            }
        }
        //No matches found!
        OnRecipeFailed?.Invoke(sender, EventArgs.Empty);
        
    }

    public List<RecipeSO> GetWaitingRecepieSOList()
    {
        return _waitingRecipeList;
    }
}
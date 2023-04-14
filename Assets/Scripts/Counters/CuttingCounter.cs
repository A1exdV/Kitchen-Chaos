using System;
using KitchenObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class CuttingCounter : BaseCounter, IHasProgress
    {
        public event EventHandler<IHasProgress.OnProgressChangedArgs> OnProgressChanged;
        
        public static event EventHandler OnAnyCut;
        public event EventHandler OnCut;


        [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

        private int _cuttingProgress;

        public override void Interact(PlayerInteractions player)
        {
            if (!HasKitchenObject())
            {
                //There is no KitchenObjects here
                if (player.HasKitchenObject())
                {
                    //Player is carrying something
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().SetKitchenCounterParent(this);
                        _cuttingProgress = 0;

                        var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                        {
                            ProgressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                        });
                    }
                }
                else
                {
                    //Player not carrying anything
                }
            }
            else
            {
                ////There is a KitchenObjects here
                if (player.HasKitchenObject())
                {
                    //Player is carrying something
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {//Player is holding a Plate
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
                else
                {
                    //Player not carrying anything
                    GetKitchenObject().SetKitchenCounterParent(player);
                }
            }
        }

        public override void InteractAlternate(PlayerInteractions player)
        {
            if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                //There is a KitchenObjects here
                _cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                {
                    ProgressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });

                if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    var outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

            return cuttingRecipeSO != null;
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
        {
            var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
            if (cuttingRecipeSO != null)
            {
                return cuttingRecipeSO.output;
            }
            else
            {
                return null;
            }
        }

        private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
            {
                if (cuttingRecipeSO.input == inputKitchenObjectSO)
                {
                    return cuttingRecipeSO;
                }
            }

            return null;
        }
    }
}
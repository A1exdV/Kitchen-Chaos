using KitchenObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
        public override void Interact(PlayerInteractions player)
        {
            if (!HasKitchenObject())
            {   //There is no KitchenObjects here
                if (player.HasKitchenObject())
                {   //Player is carrying something
                    if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                        player.GetKitchenObject().SetKitchenCounterParent(this);
                }
                else
                {  //Player not carrying anything
                    
                }
            }
            else
            { ////There is a KitchenObjects here
                if (player.HasKitchenObject())
                { //Player is carrying something
                    
                }
                else
                { //Player not carrying anything
                    GetKitchenObject().SetKitchenCounterParent(player);
                }
            }
        }

        public override void InteractAlternate(PlayerInteractions player)
        {
            if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                //There is a KitchenObjects here
                var outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
            {
                if (cuttingRecipeSO.input == inputKitchenObjectSO)
                {
                    return true;
                }
            }

            return false;
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
            {
                if (cuttingRecipeSO.input == inputKitchenObjectSO)
                {
                    return cuttingRecipeSO.output;
                }
            }

            return null;
        }
    }
}

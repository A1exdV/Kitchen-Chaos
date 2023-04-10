using Player;
using UnityEngine;

namespace Counters
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField]private KitchenObjectSO kitchenObjectSO;


        public override void Interact(PlayerInteractions player)
        {
            if (!HasKitchenObject())
            {   //There is no KitchenObjects here
                if (player.HasKitchenObject())
                {   //Player is carrying something
                    player.GetKitchenObject().SetKitchenCounterParent(this);
                }
                else
                {  //Player not carrying anything
                    
                }
            }
            else
            { ////There is KitchenObjects here
                if (player.HasKitchenObject())
                { //Player is carrying something
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {//Player is holding a Plate
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                    else
                    {//Player is not carring Plate but something else
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        { // Counter is holding a Plate
                            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            {
                                player.GetKitchenObject().DestroySelf();
                            }
                        }
                        
                    }
                }
                else
                { //Player not carrying anything
                    GetKitchenObject().SetKitchenCounterParent(player);
                }
            }
        }
    }
}

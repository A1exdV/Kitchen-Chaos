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
                    
                }
                else
                { //Player not carrying anything
                    GetKitchenObject().SetKitchenCounterParent(player);
                }
            }
        }
    }
}

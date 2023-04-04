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
            {
                if (player.HasKitchenObject())
                {
                    player.GetKitchenObject().SetKitchenCounterParent(this);
                }
                else
                {
                    
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    
                }
                else
                {
                    GetKitchenObject().SetKitchenCounterParent(player);
                }
            }
        }
    }
}

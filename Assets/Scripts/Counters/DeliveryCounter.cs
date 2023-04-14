using Player;

namespace Counters
{
    public class DeliveryCounter : BaseCounter
    {
        public override void Interact(PlayerInteractions player)
        {
            if(player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {//Only accepts Plates
                    
                    DeliveryManager.Instance.DeliverRecipe(plateKitchenObject,gameObject);
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}

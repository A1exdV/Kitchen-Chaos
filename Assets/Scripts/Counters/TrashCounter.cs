using Player;

namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(PlayerInteractions player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}

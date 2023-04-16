using System;
using Player;

namespace Counters
{
    public class TrashCounter : BaseCounter
    {

        public static event EventHandler OnAnyObjectTrashed;
        
        new public static void ResetStaticData()
        {
            OnAnyObjectTrashed = null;
        }
        public override void Interact(PlayerInteractions player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();
                OnAnyObjectTrashed?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}

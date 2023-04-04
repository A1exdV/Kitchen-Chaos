using System;
using Player;
using UnityEngine;

namespace Counters
{
    public class ContainerCounter : BaseCounter
    {

        public event EventHandler OnPlayerGrabbedObject;
        [SerializeField]private KitchenObjectSO kitchenObjectSO;

        public override void Interact(PlayerInteractions player)
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}

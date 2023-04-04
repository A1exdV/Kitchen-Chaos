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
                var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenCounterParent(player);

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}

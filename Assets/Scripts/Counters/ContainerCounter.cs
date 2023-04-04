using System;
using Player;
using UnityEngine;

namespace Counters
{
    public class ContainerCounter : BaseCounter
    {

        public event EventHandler OnPlayerGrabbedObject;
        [SerializeField]private KitchenObjectSO kitchenObjectSO;

        public override void Interact(PlayerInteractions playerInteractions)
        {
            var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenCounterParent(playerInteractions);
            
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
        }

    }
}

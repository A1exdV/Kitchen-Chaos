using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Counter
{
    public class ClearCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField]private KitchenObjectSO kitchenObjectSO;
        [SerializeField]private Transform counterTopPoint;

        private KitchenObject _kitchenObject;
        

        public void Interact(PlayerInteractions playerInteractions)
        {
            if (_kitchenObject == null)
            {
                var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
                kitchenObjectTransform.localPosition = Vector3.zero;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenCounterParent(this);
            }
            else
            {
                if(!playerInteractions.HasKitchenObject())
                    _kitchenObject.SetKitchenCounterParent(playerInteractions);
            }
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
        }

        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}

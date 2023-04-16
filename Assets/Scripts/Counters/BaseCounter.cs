using System;
using Player;
using UnityEngine;

namespace Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        public static event EventHandler OnAnyObjectPlacedHere;
        [SerializeField]private Transform counterTopPoint;
        
        private KitchenObject _kitchenObject;
        
        public static void ResetStaticData()
        {
            OnAnyObjectPlacedHere = null;
        }
        public virtual void Interact(PlayerInteractions player)
        {
            Debug.LogError("BaseCounter.Interact();");
        }
        
        public virtual void InteractAlternate(PlayerInteractions player)
        {
            //Debug.LogError("BaseCounter.InteractAlternate();");
        }
        
        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;

            if (kitchenObject != null)
            {
                OnAnyObjectPlacedHere?.Invoke(this,EventArgs.Empty);
            }
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

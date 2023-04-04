using Player;
using UnityEngine;

namespace Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField]private Transform counterTopPoint;
        
        private KitchenObject _kitchenObject;
        
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

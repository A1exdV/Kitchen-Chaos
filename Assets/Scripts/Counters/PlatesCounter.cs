using System;
using Player;
using UnityEngine;

namespace Counters
{
    public class PlatesCounter : BaseCounter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;

        [SerializeField] private float spawnPlateTimerMax = 4f;
        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
        private float _spawnPlateTimer;
        private int _platesSpawnedAmount;
        [SerializeField] private int platesSpawnedAmountMax = 4;

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;
            if (_spawnPlateTimer > spawnPlateTimerMax)
            {
                _spawnPlateTimer = 0f;

                if (_platesSpawnedAmount < platesSpawnedAmountMax)
                {
                    _platesSpawnedAmount++;
                    
                    OnPlateSpawned?.Invoke(this,EventArgs.Empty);
                }
            }
        }

        public override void Interact(PlayerInteractions player)
        {
            if (!player.HasKitchenObject())
            {
                if (_platesSpawnedAmount > 0)
                {
                    _platesSpawnedAmount--;

                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                    OnPlateRemoved?.Invoke(this,EventArgs.Empty);
                }
            }
        }
    }
}

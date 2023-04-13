using System;
using Counters;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Counter
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter baseCounter;
        [SerializeField] private GameObject[] visualGameObjectArray;
        private void Start()
        {
            PlayerInteractions.Instance.OnSelectedCounterChanged += PlayerInteractions_OnSelectedCounterChanged;
        }

        private void PlayerInteractions_OnSelectedCounterChanged(object sender, PlayerInteractions.OnSelectedCounterChangedEventArgs e)
        {
            if (e.SelectedCounter == baseCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            foreach (var visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(true); 
            }
           
        }

        private void Hide()
        {
            foreach (var visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(false); 
            }
        }
    }
}

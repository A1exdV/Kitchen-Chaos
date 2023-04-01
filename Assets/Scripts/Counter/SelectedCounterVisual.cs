using System;
using Player;
using UnityEngine;

namespace Counter
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private GameObject visualGameObject;
        private void Start()
        {
            PlayerInteractions.Instance.OnSelectedCounterChanged += PlayerInteractions_OnSelectedCounterChanged;
        }

        private void PlayerInteractions_OnSelectedCounterChanged(object sender, PlayerInteractions.OnSelectedCounterChangedEventArgs e)
        {
            if (e.SelectedCounter == clearCounter)
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
           visualGameObject.SetActive(true); 
        }

        private void Hide()
        {
            visualGameObject.SetActive(false); 
        }
    }
}

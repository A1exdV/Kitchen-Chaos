using System;
using UnityEngine;

namespace Counters.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted,
        }

        [SerializeField] private Mode mode;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(_camera.transform);
                    break;
                case Mode.LookAtInverted:
                    var directionFromCamera = transform.position - _camera.transform.position;
                    transform.LookAt(transform.position+ directionFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = _camera.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -_camera.transform.forward;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}
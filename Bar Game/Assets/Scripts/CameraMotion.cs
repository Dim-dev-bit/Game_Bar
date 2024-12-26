using UnityEngine;
using BarGame.Player;
using System;

namespace BarGame {
    public class CameraMotion : MonoBehaviour {
        [SerializeField]
        private PlayerCharacter _player;
        [SerializeField]
        private Vector3 _followCameraOffset = Vector3.zero;

        protected void Awake()
        {
            if (_player == null)
                throw new NullReferenceException($"Follow camera can't follow null character - {nameof(_player)}");
        }

        protected void LateUpdate()
        {
            if ( _player != null )
            {
                transform.position = _player.transform.position + _followCameraOffset;
            }
        }

    }
}

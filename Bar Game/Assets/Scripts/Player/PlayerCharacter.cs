using BarGame.Movement;
using UnityEngine;

namespace BarGame.Player {
    [RequireComponent(typeof(CharacterMovementController), typeof(ObjectHold))]
    public class PlayerCharacter : MonoBehaviour {
        public ObjectHold objectHold;

        private  CharacterMovementController _characterMovementController;
        private IMovementDirectionSource _movementDirectionSource;

        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovementController = GetComponent<CharacterMovementController>();

            if (objectHold == null)
                objectHold = GetComponent<ObjectHold>();
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.MovementDirection = direction;

        }
    }
}
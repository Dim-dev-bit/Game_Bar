using BarGame.Movement;
using UnityEngine;

namespace BarGame.Player {
    [RequireComponent(typeof(CharacterMovementController), typeof(ObjectHold))]
    public class PlayerCharacter : MonoBehaviour {

        private  CharacterMovementController _characterMovementController;
        private IMovementDirectionSource _movementDirectionSource;

        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.MovementDirection = direction;
        }
    }
}
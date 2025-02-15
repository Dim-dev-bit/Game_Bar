using BarGame.Movement;
using UnityEngine;

namespace BarGame.Player {
    [RequireComponent(typeof(CharacterMovementController), typeof(InteractionWithObjects))]
    public class PlayerCharacter : MonoBehaviour {
        public InteractionWithObjects objectHold;

        private  CharacterMovementController _characterMovementController;
        private IMovementDirectionSource _movementDirectionSource;

        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovementController = GetComponent<CharacterMovementController>();

            if (objectHold == null)
                objectHold = GetComponent<InteractionWithObjects>();
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.MovementDirection = direction;

        }
    }
}
using BarGame.Movement;
using BarGame.Player.Interactions;
using UnityEngine;

namespace BarGame.Player {
    [RequireComponent(typeof(CharacterMovementController), typeof(InteractionWithObjects))]
    public class PlayerCharacter : MonoBehaviour {
        public InteractionWithObjects InteractionWithObjects;
        public PickUpHandler PickUpHandler;
        public ActionHandler ActionHandler;

        private  CharacterMovementController _characterMovementController;
        private IMovementDirectionSource _movementDirectionSource;

        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovementController = GetComponent<CharacterMovementController>();

            if (InteractionWithObjects == null)
                InteractionWithObjects = GetComponent<InteractionWithObjects>();
            if (PickUpHandler == null)
                PickUpHandler = GetComponent<PickUpHandler>();
            if (ActionHandler == null) 
                ActionHandler = GetComponent<ActionHandler>();


        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.MovementDirection = direction;

        }
    }
}
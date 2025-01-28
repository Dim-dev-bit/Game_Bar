using UnityEngine;

namespace BarGame.Movement {
    [RequireComponent (typeof(PlayerControls))]

    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource {
        public Vector2 MovementDirection {  get; private set; }

        private PlayerControls playerControls;
        private Animator myAnimator;
        private ObjectHold _objectHold;

        protected void Awake()
        {
            myAnimator = GetComponent<Animator>();
            playerControls = new PlayerControls ();
            _objectHold = GetComponent<ObjectHold>();
            _objectHold.canMove = true;

        }

        protected void OnEnable()
        {
            playerControls.Enable();
        }

        protected void Update()
        {
            PlayerInput();
        }
        private void PlayerInput()
        {
            if (!_objectHold.canMove) return;
            MovementDirection = playerControls.Player.Move.ReadValue<Vector2>();

            myAnimator.SetFloat("moveX", MovementDirection.x);
            myAnimator.SetFloat("moveY", MovementDirection.y);
        }

        
    }
}
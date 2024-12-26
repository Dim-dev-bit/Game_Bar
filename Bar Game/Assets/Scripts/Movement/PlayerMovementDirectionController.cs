using UnityEngine;

namespace BarGame.Movement {
    [RequireComponent (typeof(PlayerControls))]

    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource {
        public Vector2 MovementDirection {  get; private set; }

        private PlayerControls playerControls;
        private Animator myAnimator;

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
            playerControls = new PlayerControls ();
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void Update()
        {
            PlayerInput();
        }
        private void PlayerInput()
        {
            MovementDirection = playerControls.Player.Move.ReadValue<Vector2>();

            myAnimator.SetFloat("moveX", MovementDirection.x);
            myAnimator.SetFloat("moveY", MovementDirection.y);
        }

        
    }
}
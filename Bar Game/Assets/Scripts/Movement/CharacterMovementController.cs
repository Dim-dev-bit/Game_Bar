using BarGame.Player.Interactions;
using UnityEngine;

namespace BarGame.Movement {
    [RequireComponent (typeof(Rigidbody2D))]
    public class CharacterMovementController : MonoBehaviour {
        public Transform holdPosition;

        [SerializeField] 
        private float moveSpeed = 4f;
        public Vector2 MovementDirection { get; set; }
        public SpriteRenderer mySpriteRenderer;
        private ActionHandler _actionHandler;
        private Rigidbody2D _rb;

        protected void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            mySpriteRenderer = _rb.GetComponent<SpriteRenderer>();
            _actionHandler = _rb.GetComponent<ActionHandler>();
            _actionHandler.canMove = true;

        }

        protected void FixedUpdate()
        {
            AdjustPlayerFacingDirection();
            Move();
        }

        private void Move()
        {
            if (!_actionHandler.canMove) return;
            _rb.MovePosition(_rb.position + MovementDirection * (moveSpeed * Time.fixedDeltaTime));
        }

        private void AdjustPlayerFacingDirection()
        {
            if (!_actionHandler.canMove) return;
            if (MovementDirection.x < 0)
            {
                mySpriteRenderer.flipX = true;
                holdPosition.transform.localPosition = new Vector3(-0.5f, 0.25f, 0);
            }
            else if (MovementDirection.x > 0)
            {
                mySpriteRenderer.flipX = false;
                holdPosition.transform.localPosition = new Vector3(0.5f, 0.25f, 0);
            }
        }
    }
}
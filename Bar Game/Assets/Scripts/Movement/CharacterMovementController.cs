using UnityEngine;

namespace BarGame.Movement {
    [RequireComponent (typeof(Rigidbody2D))]
    public class CharacterMovementController : MonoBehaviour {
        public Transform holdPosition;

        [SerializeField] 
        private float moveSpeed = 4f;
        public Vector2 MovementDirection { get; set; }
        public SpriteRenderer mySpriteRenderer;
        private ObjectHold _objectHold;
        private Rigidbody2D _rb;

        protected void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            mySpriteRenderer = _rb.GetComponent<SpriteRenderer>();
            _objectHold = _rb.GetComponent<ObjectHold>();
            _objectHold.canMove = true;

        }

        protected void FixedUpdate()
        {
            AdjustPlayerFacingDirection();
            Move();
        }

        private void Move()
        {
            if (!_objectHold.canMove) return;
            _rb.MovePosition(_rb.position + MovementDirection * (moveSpeed * Time.fixedDeltaTime));
        }

        private void AdjustPlayerFacingDirection()
        {
            if (!_objectHold.canMove) return;
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
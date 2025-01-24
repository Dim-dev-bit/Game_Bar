using UnityEngine;

namespace BarGame.Movement {
    [RequireComponent (typeof(Rigidbody2D))]
    public class CharacterMovementController : MonoBehaviour {
        public Transform holdPosition;

        [SerializeField] 
        private float moveSpeed = 4f;
        public Vector2 MovementDirection { get; set; }
        public SpriteRenderer mySpriteRenderer;
        private Rigidbody2D rb;

        protected void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            mySpriteRenderer = rb.GetComponent<SpriteRenderer>();
        }

        protected void FixedUpdate()
        {
            AdjustPlayerFacingDirection();
            Move();
        }

        private void Move()
        {
            rb.MovePosition(rb.position + MovementDirection * (moveSpeed * Time.fixedDeltaTime));
        }

        private void AdjustPlayerFacingDirection()
        {
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
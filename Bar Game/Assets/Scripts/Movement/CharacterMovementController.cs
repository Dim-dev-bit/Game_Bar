using UnityEngine;

namespace BarGame.Movement {
    [RequireComponent (typeof(Rigidbody2D))]
    public class CharacterMovementController : MonoBehaviour {
        public Transform holdPosition;

        [SerializeField] 
        private float moveSpeed = 4f;
        public Vector2 MovementDirection { get; set; }
        private SpriteRenderer mySpriteRenderer;
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
            Vector3 mousePos = Input.mousePosition;
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

            if (mousePos.x < playerScreenPoint.x)
            {
                mySpriteRenderer.flipX = true;
                holdPosition.transform.localPosition = new Vector3(-0.5f, 0, 0);
            }
            else
            {
                mySpriteRenderer.flipX = false;
                holdPosition.transform.localPosition = new Vector3(0.5f, 0, 0);
            }
        }
    }
}
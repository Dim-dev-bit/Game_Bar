using UnityEngine;

namespace BarGame {
    public class ObjectHold : MonoBehaviour {
        public bool IsHold;
        public float distance = 2f;
        public Transform holdPoint;
        RaycastHit2D hit;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!IsHold)
                {
                    Physics2D.queriesStartInColliders = false; 
                    if (IsFacingRight())
                        hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
                    else
                        hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance);


                    if (hit.collider != null && hit.collider.tag == TagUtils.PickUpTagName)
                        IsHold = true;
                }
            } 

            if (IsHold)
            {
                hit.collider.gameObject.transform.position = holdPoint.position;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
        }

        private bool IsFacingRight()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

            if (mousePos.x > playerScreenPoint.x)
                return true;
            else
                return false;
        }
    }
}
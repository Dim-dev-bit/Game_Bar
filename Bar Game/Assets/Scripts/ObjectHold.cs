using UnityEngine;

namespace BarGame {
    public class ObjectHold : MonoBehaviour {
        public bool IsHold;

        public Transform holdPoint;

        private Collider2D[] _colliders = new Collider2D[2];
        private GameObject _pickUp;

        [SerializeField]
        private float _lookRadius = 1f;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!IsHold)
                {
                    _pickUp = GetPickUp();
                    if (_pickUp != null)
                        IsHold = true;
                }
                else
                    IsHold = false;

            }
            if (IsHold)
                _pickUp.transform.position = holdPoint.position;
        }

        private GameObject GetPickUp()
        {
            GameObject pickUp = null;

            var position = transform.position;
            var radius = _lookRadius;
            var mask = LayerUtils.PickUpLayer;

            var size = Physics2D.OverlapCircleNonAlloc(position, radius, _colliders, mask);
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject)
                    {
                        pickUp = _colliders[i].gameObject;
                        break;
                    }
                }
            }
            return pickUp;
        }
    }
}
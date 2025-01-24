using BarGame.Furniture;
using UnityEngine;

namespace BarGame {
    public class ObjectHold : MonoBehaviour {
        public bool IsHold;

        public Transform holdPoint;
        public SpriteRenderer mySpriteRenderer;
        public Table table;

        private GameObject _pickUp;
        private RaycastHit2D _hit1;
        private RaycastHit2D _hit2;
        private string _tag;

        [SerializeField]
        private float _lookRadius = 2f;


        protected void Awake()
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!IsHold)
                {
                    _pickUp = GetPickUp();
                    if (_pickUp != null)
                    {
                        _tag = _pickUp.tag;
                        IsHold = true;
                    }
                }
                else if (_tag == "Bottle")
                {
                    if (table != null)
                    {
                        bool placed = table.PlaceItem(_pickUp);
                        if (placed)
                            IsHold = false;
                    }
                }
            }
            if (IsHold)
            {
                _pickUp.transform.position = holdPoint.position;
            }
        }

        public void SetCurrentTable(Table otherTable)
        {
            table = otherTable;
        }

        private GameObject GetPickUp()
        {
            GameObject pickUp = null;
            var mask = LayerUtils.PickUpLayer;

            Physics2D.queriesStartInColliders = false;
            if (mySpriteRenderer.flipX)
                _hit1 = Physics2D.Raycast(holdPoint.transform.position, -Vector2.right, _lookRadius, mask);
            else
                _hit1 = Physics2D.Raycast(holdPoint.transform.position, Vector2.right, _lookRadius, mask);

            _hit2 = Physics2D.Raycast(transform.position - Vector3.up * _lookRadius, Vector2.up, _lookRadius * 2, mask);
            if (_hit1.collider != null)
                pickUp = _hit1.collider.gameObject;
            if (_hit2.collider != null)
                pickUp = _hit2.collider.gameObject;
            Debug.Log(pickUp);
            return pickUp;
        }        
    }
}
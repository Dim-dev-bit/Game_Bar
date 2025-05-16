using BarGame.Furniture;
using UnityEngine;

namespace BarGame.Player.Interactions {
    public class PickUpHandler : MonoBehaviour {

        public bool IsHold;
        public GameObject PickUp {  get; private set; }
        public float LookDistance {  get; private set; }
        public Table table;

        private string _tag;
        private SpriteRenderer _mySpriteRenderer;
        public Transform holdPoint;

        protected void Awake()
        {
            IsHold = false;
        }
        public void Initialize(SpriteRenderer mySpriteRenderer, Transform holdPosition, float lookDistance)
        {
            _mySpriteRenderer = mySpriteRenderer;
            holdPoint = holdPosition;
            LookDistance = lookDistance;
        }

        public void SetCurrentTable(Table otherTable)
        {
            table = otherTable;
        }

        public void SetCurrentPickUp(GameObject pickUp)
        {
            PickUp = pickUp;
            IsHold = true;
            _tag = PickUp.tag;
        }

        public void DestroyCurrentPickUp()
        {
            Destroy(PickUp);
            IsHold = false;
            _tag = null;
            PickUp = null;
        }

        public GameObject GetPickUp()
        {
            GameObject pickUp = null;
            var mask = LayerUtils.PickUpLayer;
            RaycastHit2D _hit1;
            RaycastHit2D _hit2;

            Physics2D.queriesStartInColliders = false;
            if (_mySpriteRenderer.flipX)
                _hit1 = Physics2D.Raycast(transform.position, -Vector2.right, LookDistance, mask);
            else
                _hit1 = Physics2D.Raycast(transform.position, Vector2.right, LookDistance, mask);

            _hit2 = Physics2D.Raycast(transform.position - Vector3.up * LookDistance, Vector2.up, LookDistance * 2, mask);
            if (_hit2.collider != null)
                pickUp = _hit2.collider.gameObject;
            if (_hit1.collider != null)
                pickUp = _hit1.collider.gameObject;

            return pickUp;
        }

        public void Basic()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!IsHold)
                {
                    PickUp = GetPickUp();
                    if (PickUp != null)
                    {
                        PickUp.layer = LayerUtils.PickedUpLayerNum;
                        _tag = PickUp.tag;
                        IsHold = true;
                    }
                }
                else if (TagUtils.PickUps.Contains(_tag))
                {
                    if (table != null)
                    {
                        bool placed = table.PlaceItem(PickUp);
                        if (placed)
                        {
                            PickUp.layer = LayerUtils.PickUpLayerNum;
                            IsHold = false;
                            _tag = null;
                            PickUp = null;
                        }
                    }
                }
            }
            if (IsHold) {
                if (PickUp != null)
                    PickUp.transform.position = holdPoint.position;
                else
                {
                    IsHold = false;
                }
            }
        }
    }
}
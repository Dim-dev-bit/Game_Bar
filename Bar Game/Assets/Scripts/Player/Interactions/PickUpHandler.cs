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

            Collider2D[] pickUps = Physics2D.OverlapCircleAll(transform.position, LookDistance, mask);

            if (pickUps.Length == 0)
                return null;
            
            float closest = Mathf.Infinity;
            foreach (var hit in pickUps)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closest)
                {
                    closest = distance;
                    pickUp = hit.gameObject;
                }
            }
            

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
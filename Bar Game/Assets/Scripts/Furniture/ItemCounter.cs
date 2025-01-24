using UnityEngine;

namespace BarGame.Furniture {
    public class ItemCounter : MonoBehaviour {
        [SerializeField]
        private Table _table;

        private Collider2D[] _coll;
        private Vector2 _size = new Vector2(2, 0.5f);
        
        public int numOfItems()
        {
            _coll = Physics2D.OverlapBoxAll(transform.position, _size, 0f, LayerUtils.PickUpLayer);
            return _coll.Length;
        }
    }
}
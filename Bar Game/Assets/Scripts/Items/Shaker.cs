using UnityEngine;

namespace BarGame.Items {
    public class Shaker : MonoBehaviour {
        public Sprite newSprite;

        private SpriteRenderer _spriteRenderer;

        protected void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeSprite()
        {
            _spriteRenderer.sprite = newSprite;
        }

        // Пока на стадии разработки...
    }
}
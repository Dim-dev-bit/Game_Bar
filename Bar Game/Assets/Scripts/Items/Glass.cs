using BarGame.Player;
using BarGame.Player.Interactions;
using UnityEngine;

namespace BarGame.Items {
    public class Glass : MonoBehaviour {

        public Sprite newSprite;

        private SpriteRenderer _spriteRenderer;
        private enum Actions
        {
            Filling,
            Shaking,
            Stirring
        };


        private void OnEnable()
        {
            ActionHandler.OnCompletingAction += DoStuff;
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName)) { 
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (player != null) { 
                    player.ActionHandler.SetCurrentGlass(this);
                }
            }
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (player != null)
                    player.ActionHandler.SetCurrentGlass(null);
            }
        }
        protected void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeSprite()
        {
            _spriteRenderer.sprite = newSprite;
        }

        public void DoStuff() {

            Debug.Log("Stuff");
        }
    }
}
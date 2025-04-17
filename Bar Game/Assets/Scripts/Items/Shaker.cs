using BarGame.Player;
using System.Collections.Generic;
using UnityEngine;

namespace BarGame.Items {
    public class Shaker : MonoBehaviour {
        public Sprite newSprite;

        public List<string> ShakingActions;

        private SpriteRenderer _spriteRenderer;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (player != null)
                    player.ActionHandler.SetCurrentShaker(this);
            }
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (player != null)
                    player.ActionHandler.SetCurrentShaker(null);
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

        public void AddToGlass(List<string> glassList)
        {
            glassList.Add("shaking");
            foreach (string action in ShakingActions)
            {
                glassList.Add(action);
            }

            ShakingActions.Clear();
        }

    }
}
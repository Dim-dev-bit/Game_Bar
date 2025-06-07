using BarGame.Player;
using BarGame.Player.Interactions;
using System.Collections.Generic;
using UnityEngine;

namespace BarGame.Items {
    public class Glass : MonoBehaviour {

        public Sprite newSprite;
        [SerializeField]public string type;

        [SerializeField]public List<string> RecipeToMatch;

        private PlayerCharacter _player;
        private GameObject _ingredient;

        private SpriteRenderer _spriteRenderer;
        private enum Actions
        {
            Filling,
            Shaking,
            Stirring
        };

        //protected void Update()
        //{
        //    if (_player != null && Input.GetKeyDown(KeyCode.DownArrow) && _ingredient != null)
        //    {
        //        RecipeToMatch.Add(_ingredient.tag);
        //        _player.PickUpHandler.DestroyCurrentPickUp();
        //    }
        //}


        private void OnEnable()
        {
            ActionHandler.OnCompletingAction += DoStuff;
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName)) { 
                _player = other.GetComponent<PlayerCharacter>();
                if (_player != null) { 
                    _player.ActionHandler.SetCurrentGlass(this);
                }
                GameObject item = _player.PickUpHandler.PickUp;
                if (item != null && TagUtils.IsIngredient(item))
                {
                    _ingredient = item;
                }

            }
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                if (_player != null)
                    _player.ActionHandler.SetCurrentGlass(null);
                _player = null;
                _ingredient = null;
            }
        }
        protected void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeSprite()
        {
            _spriteRenderer.sprite = newSprite;
            //this.gameObject.layer = LayerUtils.
        }

        public void DoStuff() {

            Debug.Log("Stuff");
        }
    }
}
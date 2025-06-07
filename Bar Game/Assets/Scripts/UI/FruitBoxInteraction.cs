using BarGame.Player;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Assets.Scripts.Utils;
using BarGame.Player.Interactions;

namespace BarGame.UI {
    public class FruitBox : MonoBehaviour {
        [SerializeField] private Text _hintText;
        [SerializeField] private Text _infoText;
        [SerializeField] private string _tagName;
        [SerializeField] private GameObject _prefabName;
        private PlayerCharacter _player;
        private GameObject _currentFruit;

        private int _maxAmount = 10;
        private int _currentAmount = 5;

        private bool _isPlayerNear = false;


        protected void Start()
        {
            _infoText.gameObject.SetActive(false);
            _hintText.gameObject.SetActive(false);
        }
        protected void Update()
        {
            if (_isPlayerNear)
            {
                _infoText.text = $"{_currentAmount}/{_maxAmount}";
                _hintText.text = "Use an Up Arrow to take a fruit from a fruit box.\nUp arrow to place a fruit in the box.";
            }
            if (_isPlayerNear && _currentFruit != null && Input.GetKeyDown(KeyCode.UpArrow) && _currentAmount < _maxAmount)
            {
                _player.PickUpHandler.DestroyCurrentPickUp();
                _currentFruit = null;
                _currentAmount++;
            }
            else if (_isPlayerNear && _currentFruit == null && Input.GetKeyDown(KeyCode.UpArrow) && _currentAmount != 0)
            {
                GameObject fruit = Instantiate(_prefabName, _player.PickUpHandler.holdPoint);
                _player.PickUpHandler.SetCurrentPickUp(fruit);
                fruit.transform.SetParent(null);
                _currentAmount--;
            }
        }

        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                _player = other.GetComponent<PlayerCharacter>();
                _isPlayerNear = true;
                _infoText.gameObject.SetActive(true);
                _hintText.gameObject.SetActive(true);
                GameObject pickUp = _player.PickUpHandler.PickUp;
                if (pickUp != null && pickUp.tag == _tagName)
                {
                    _currentFruit = pickUp;
                }
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                _currentFruit = null;
                _isPlayerNear = false;
                _player = null;
                _infoText.gameObject.SetActive(false);
                _hintText.gameObject.SetActive(false);
            }
        }
    }
}
using BarGame.Player;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Assets.Scripts.Utils;

namespace BarGame.UI {
    public class FruitBox : MonoBehaviour {
        public ConversionObject _currentConversionObject;

        private GameObject _currentFruit;
        private GameObject _currentChosenFruit;
        private PlayerCharacter _player;
        private Coroutine _coroutine;
        [SerializeField] private GameObject _Menu;
        [SerializeField] private Text _hintText;
        [SerializeField] private Text _infoText;
        private Dictionary<string, GameObject> _connections;

        public Dictionary<string, int> fruitsCounts = new Dictionary<string, int>();
        private bool _showInfoText = false;
        private bool _isPlayerNear = false;

        private int _curChoice = 0;

        protected void Start()
        {
            _connections = new Dictionary<string, GameObject>();
            foreach (var element in _currentConversionObject.conversions)
            {
                _connections.Add(element.name, element.prefab);
            }
            _Menu.SetActive(false);
            _infoText.gameObject.SetActive(false);
            _hintText.gameObject.SetActive(false);
        }
        protected void Update()
        {
            if (_isPlayerNear)
            {
                _hintText.text = "Use L to open the Box, Up \nand Down to scroll through fruits, \nEnter to choose a fruit \nor X to exit choosing mode \nand K to place a fruit.";
            }
            if (_isPlayerNear && Input.GetKeyDown(KeyCode.L) && !_showInfoText && !_player.PickUpHandler.IsHold)
            {
                _showInfoText = true;
                _coroutine = StartCoroutine(StartingChoosing());
            }
            if (_isPlayerNear && _currentFruit != null && Input.GetKeyDown(KeyCode.K))
            {
                if (fruitsCounts.ContainsKey(_currentFruit.tag))
                {
                    fruitsCounts[_currentFruit.tag]++;
                }
                else
                {
                    fruitsCounts.Add(_currentFruit.tag, 1);
                }
                _player.PickUpHandler.DestroyCurrentPickUp();
                UpdateMenuDisplay(_curChoice);
            }
        }

        private void UpdateMenuDisplay(int highlightIndex = 0)
        {
            if (fruitsCounts.Count == 0) return;

            _curChoice = Mathf.Clamp(highlightIndex, 0, fruitsCounts.Count - 1);

            _infoText.text = "";

            var sorted = fruitsCounts.OrderBy(kvp => kvp.Key).ToList();

            _infoText.text = "";
            for (int i = 0; i < sorted.Count; i++)
            {
                if (i == highlightIndex)
                    _currentChosenFruit = _connections[sorted[i].Key];
                _infoText.text += (i == highlightIndex)
                    ? $"<{sorted[i].Key} x{sorted[i].Value}>\n"
                    : $"{sorted[i].Key} x{sorted[i].Value}\n";
            }
        }

        private IEnumerator StartingChoosing()
        {
            if (fruitsCounts.Count == 0)
            {
                _showInfoText = false;
                yield break;
            }

            _showInfoText = true;
            _infoText.gameObject.SetActive(true);
            _curChoice = 0;
            UpdateMenuDisplay(_curChoice);

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _curChoice = (_curChoice - 1 + fruitsCounts.Count) % fruitsCounts.Count;
                    UpdateMenuDisplay(_curChoice);
                    yield return new WaitForSeconds(0.15f);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _curChoice = (_curChoice + 1) % fruitsCounts.Count;
                    UpdateMenuDisplay(_curChoice);
                    yield return new WaitForSeconds(0.15f);
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    SetCurrentFruit(_currentChosenFruit);
                    _infoText.gameObject.SetActive(false);
                    yield break;
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    _infoText.gameObject.SetActive(false);
                    yield break;
                }

                _showInfoText = false;
                yield return null;
            }
        }

        private void SetCurrentFruit(GameObject ChosenFruit)
        {
            if (_player == null || !fruitsCounts.ContainsKey(ChosenFruit.tag)) return;
            {
                if (_player.PickUpHandler.PickUp != null)
                    Destroy(_player.PickUpHandler.PickUp);
                GameObject holdingFruit = Instantiate(ChosenFruit, _player.PickUpHandler.holdPoint);
                holdingFruit.transform.SetParent(null);

                _player.PickUpHandler.SetCurrentPickUp(holdingFruit);

                fruitsCounts[ChosenFruit.tag]--;
                if (fruitsCounts[ChosenFruit.tag] == 0)
                    fruitsCounts.Remove(ChosenFruit.tag);
            }
        }
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                _player = other.GetComponent<PlayerCharacter>();
                _isPlayerNear = true;
                _Menu.SetActive(_isPlayerNear);
                _hintText.gameObject.SetActive(_isPlayerNear);
                GameObject pickUp = _player.PickUpHandler.PickUp;
                if (pickUp != null && TagUtils.IsFruit(pickUp))
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
                if (_coroutine != null)
                {
                    StopCoroutine( _coroutine );
                    _coroutine = null;
                }
                _Menu.SetActive(_isPlayerNear);
            }
        }
    }
}
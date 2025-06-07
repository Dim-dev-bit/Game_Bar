using Assets.Scripts.Utils;
using BarGame.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BarGame.Items {
    public class Juicer : MonoBehaviour {
        public ConversionObject _currentConversionObject;
        public Animator juicerAnimator;

        private int _whichFruit = 0;

        private PlayerCharacter _player;

        private string _currentFruitName;
        private string _juicingFruitName;
        private bool _isPlayerNear = false;
        private bool _isJuicing = false;

        private float _maxTimeOfJuicingSec = 3f;
        private float _currentTimeOfJuicingSec;

        [SerializeField] private Transform _outputPosition;
        [SerializeField] private Text _hintText;
        private Dictionary<string, GameObject> _connections;

        protected void Awake()
        {
            _hintText.gameObject.SetActive(false);
            _connections = new Dictionary<string, GameObject>();
            foreach (var element in _currentConversionObject.conversions) {
                _connections.Add(element.name, element.prefab);
            }
            _currentTimeOfJuicingSec = 0f;
        }
        protected void Update()
        {
            if (_isPlayerNear)
                _hintText.text = "Fill with juice using Up Arrow \nand wait for 3 seconds for a juice.";
            if (_isPlayerNear && _currentFruitName != null && !_isJuicing && Input.GetKeyDown(KeyCode.UpArrow))
            {
                _player.PickUpHandler.DestroyCurrentPickUp();
                _juicingFruitName = _currentFruitName;
                _currentFruitName = null;
                _isJuicing = true;
                if (_juicingFruitName == "Orange")
                    _whichFruit = 1;
                else if (_juicingFruitName == "Lime")
                    _whichFruit = 2;
                else if (_juicingFruitName == "Papaya")
                    _whichFruit = 3;
            }
            if (_isJuicing)
            {
                Debug.Log(_whichFruit);
                juicerAnimator.SetInteger("WhichFruit", _whichFruit);
                _currentTimeOfJuicingSec += Time.deltaTime;
            }
            if (_currentTimeOfJuicingSec > _maxTimeOfJuicingSec)
            {
                juicerAnimator.SetInteger("WhichFruit", 0);
                _isJuicing = false;
                GameObject juice = Instantiate(_connections[_juicingFruitName], new Vector3(_outputPosition.position.x, _outputPosition.position.y, 0), Quaternion.identity);
                juice.transform.SetParent(null);
                if (juice.transform.childCount > 0)
                {
                    juice.transform.GetChild(0).gameObject.SetActive(true);
                }

                _currentTimeOfJuicingSec = 0f;
            }
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagUtils.PlayerTagName))
            {
                _hintText.gameObject.SetActive(true);
                _isPlayerNear = true;
                _player = collision.GetComponent<PlayerCharacter>();
                GameObject item = _player.PickUpHandler.PickUp;
                if (item != null)
                {
                    if (TagUtils.IsFruit(item))
                        _currentFruitName = item.tag;
                }
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagUtils.PlayerTagName))
            {
                _hintText.gameObject.SetActive(false);
                _player = null;
                _isPlayerNear = false;
                _currentFruitName = null;
            }
        }
    }
}
using BarGame.Furniture;
using BarGame.Items;
using System.Collections.Generic;
using UnityEngine;

namespace BarGame {
    public class InteractionWithObjects : MonoBehaviour {
        public bool IsHold;
        public bool canMove;

        public Transform holdPoint;
        public SpriteRenderer mySpriteRenderer;

        public Table table;
        public Shaker shaker;
        public Glass glass;

        [SerializeField]
        private State _startingState;
        [SerializeField]
        private float _lookDistance = 1f;

        private GameObject _pickUp;
        private GameObject _nearObject;
        private RaycastHit2D _hit1;
        private RaycastHit2D _hit2;
        private string _tag;
        private State _currentState;

        private float _timerInSec = 0f;
        private float _timerMax = 3f;
        private bool _isPouring = false;

        private List<KeyCode> playerInput = new List<KeyCode>();

        public enum State
        {
            Basic,
            Filling,
            Shaking,
            Stirring
        }
        
        protected void Awake()
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            _currentState = _startingState;
        }

        protected void Update()
        {
            StateHandler();
        }

        public void SetCurrentTable(Table otherTable) {
            table = otherTable;
        }

        public void SetCurrentShaker(Shaker otherShaker) {
            shaker = otherShaker;
        }

        public void SetCurrentGlass(Glass otherGlass) {
            glass = otherGlass;
        }


        private void StateHandler()
        {
            switch (_currentState)
            {
                case State.Shaking:
                    Action(Combinations.shakingSequence);
                    break;
                case State.Filling:
                    Filling();
                    CheckCurrentState();
                    break;
                case State.Stirring:
                    Action(Combinations.stirringSequence);
                    break;
                default:
                case State.Basic:
                    Basic();
                    CheckCurrentState();
                    break;
            }
        }

        private void CheckCurrentState()
        {
            _nearObject = GetPickUp();
            if (_nearObject != null)
            {
                if (_currentState == State.Basic && IsHold && _pickUp.CompareTag(TagUtils.SpoonTagName) && _nearObject.CompareTag(TagUtils.GlassTagName) && Input.GetKeyDown(KeyCode.F))
                    _currentState = State.Stirring;
                else if (_currentState == State.Basic && IsHold && _pickUp.CompareTag(TagUtils.BottleTagName) && _nearObject.CompareTag(TagUtils.GlassTagName) && Input.GetKeyDown(KeyCode.H))
                    _currentState = State.Filling;
            }
            if (_currentState == State.Basic && IsHold && _pickUp.CompareTag(TagUtils.ShakerTagName) && Input.GetKeyDown(KeyCode.G))
                _currentState = State.Shaking;
        }

        private GameObject GetPickUp()
        {
            GameObject pickUp = null;
            var mask = LayerUtils.PickUpLayer;

            Physics2D.queriesStartInColliders = false;
            if (mySpriteRenderer.flipX)
                _hit1 = Physics2D.Raycast(transform.position, -Vector2.right, _lookDistance, mask);
            else
                _hit1 = Physics2D.Raycast(transform.position, Vector2.right, _lookDistance, mask);

            _hit2 = Physics2D.Raycast(transform.position - Vector3.up * _lookDistance, Vector2.up, _lookDistance * 2, mask);
            if (_hit2.collider != null)
                pickUp = _hit2.collider.gameObject;
            if (_hit1.collider != null)
                pickUp = _hit1.collider.gameObject;
     
            return pickUp;
        }

        private void Basic()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!IsHold)
                {
                    _pickUp = GetPickUp();
                    if (_pickUp != null)
                    {
                        _pickUp.layer = LayerUtils.PickedUpLayerNum;
                        _tag = _pickUp.tag;
                        IsHold = true;
                    }
                }
                else if (_tag == TagUtils.BottleTagName || _tag == TagUtils.ShakerTagName || _tag == TagUtils.SpoonTagName || _tag == TagUtils.GlassTagName) // add list instead of this horror...
                {
                    if (table != null)
                    {
                        bool placed = table.PlaceItem(_pickUp);
                        if (placed)
                        {
                            _pickUp.layer = LayerUtils.PickUpLayerNum;
                            IsHold = false;
                            _tag = null;
                        }
                    }
                }
            }
            if (IsHold)
                 _pickUp.transform.position = holdPoint.position;

        }

        private void Action(List<KeyCode> sequence)
        {
            canMove = false;
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        playerInput.Add(key);
                        Debug.Log($"Pressed: {key}");

                        if (playerInput.Count > sequence.Count)
                            playerInput.RemoveAt(0);

                        CheckSequence(sequence);
                    }
                }
            }
        }
        private void Filling()
        {
            if (Input.GetKey(KeyCode.H)) {
                if (!_isPouring)
                {
                    _isPouring = true;
                    _timerInSec = 0;
                }
                Debug.Log(_timerInSec);
                _timerInSec += Time.deltaTime;
                if (_timerInSec >= _timerMax)
                {
                    Debug.Log("Poured!");
                    _timerInSec = 0;
                    _isPouring = false;
                    glass.ChangeSprite();
                    _currentState = State.Basic;
                }
            }
            else
            {
                _isPouring = false;
                _timerInSec = 0;
                _currentState = State.Basic;
            }
        }
        private void CheckSequence(List<KeyCode> sequence)
        {
            if (playerInput.Count == sequence.Count)
            {
                bool isMatch = true;
                for (int i = 0; i < playerInput.Count; i++)
                    if (playerInput[i] != sequence[i]) {
                        isMatch = false;
                        break;
                    }

                if (isMatch) {
                    if (shaker != null)
                        shaker.ChangeSprite();
                    else if (glass != null)
                        glass.ChangeSprite();
                    Debug.Log("Done!");
                    playerInput.Clear();
                    _currentState = State.Basic;
                    canMove = true;
                }

            }
        }
    }
}
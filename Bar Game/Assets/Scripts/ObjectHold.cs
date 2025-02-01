using BarGame.Furniture;
using System.Collections.Generic;
using UnityEngine;

namespace BarGame {
    public class ObjectHold : MonoBehaviour {
        public bool IsHold;
        public bool canMove;

        public Transform holdPoint;
        public SpriteRenderer mySpriteRenderer;
        public Table table;

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

        public void SetCurrentTable(Table otherTable)
        {
            table = otherTable;
        }

        private void StateHandler()
        {
            switch (_currentState)
            {
                case State.Shaking:
                    Shaking();
                    break;
                case State.Filling:
                    CheckCurrentState();
                    break;
                case State.Stirring:
                    Stirring();
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
                if (_currentState == State.Basic && IsHold && _pickUp.CompareTag(TagUtils.SpoonTagName) && _nearObject.CompareTag(TagUtils.ShakerTagName) && Input.GetKeyDown(KeyCode.F))
                    _currentState = State.Stirring;
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
                else if (_tag == TagUtils.BottleTagName || _tag == TagUtils.ShakerTagName || _tag == TagUtils.SpoonTagName)
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

        private void Stirring()
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

                        if (playerInput.Count > Combinations.stirringSequence.Count)
                        {
                            playerInput.RemoveAt(0);
                        }

                        CheckSequence(Combinations.stirringSequence);
                    }
                }
            }
        }

        private void Shaking()
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

                        if (playerInput.Count > Combinations.shakingSequence.Count)
                        {
                            playerInput.RemoveAt(0);
                        }

                        CheckSequence(Combinations.shakingSequence);
                    }
                }
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

                if (isMatch)
                {
                    Debug.Log("Cool");
                    playerInput.Clear();
                    _currentState = State.Basic;
                    canMove = true;

                }

            }
        }
    }
}
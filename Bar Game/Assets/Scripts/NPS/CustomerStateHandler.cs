using BarGame.Player;
using BarGame.UI;
using UnityEngine;

namespace BarGame.NPS {
    public class CustomerStateHandler : MonoBehaviour {

        public enum State {
            Searching,
            Phrasing,
            OrderWaiting,
            Leaving
        }

        private State _startingState;
        private State _currentState;

        private PathFindingLogic _pathFindingLogic;
        private DialogueDisplayer _dialogueDisplayer;
        private MovingDialogueBox _movingDialogueBox;
        private PlayerCharacter _player;

        private Rigidbody2D _rb;

        private string order;
        protected void Awake()
        {
            _startingState = State.Searching;
            _currentState = _startingState;

            _pathFindingLogic = GetComponent<PathFindingLogic>();
            _dialogueDisplayer = FindObjectOfType<DialogueDisplayer>();

            _rb = GetComponent<Rigidbody2D>();

            _rb.gravityScale = 0;
        }

        public void HandleState()
        {
            switch (_currentState)
            {
                case State.Phrasing:
                    Phrasing();
                    CheckCurrentState();
                    break;
                case State.OrderWaiting:
                    OrderWaiting();
                    break;
                case State.Leaving:
                    Leaving();
                    break;
                default:
                case State.Searching:
                    _pathFindingLogic.FindingSeat();
                    CheckCurrentState();
                    break;
            }
        }

        private void CheckCurrentState()
        {
            if (_currentState == State.Searching && _pathFindingLogic.IsStopped)
            {
                _currentState = State.Phrasing;
                _rb.velocity = new Vector2(0f, 0f);
            }
            else if (_currentState == State.Phrasing && _dialogueDisplayer.dialogueFinished && _player != null) {
                _currentState = State.OrderWaiting;
                order = _dialogueDisplayer.orderPhrase;
                Debug.Log(order);
                _dialogueDisplayer.dialogueFinished = false;
            }
        }

        private void Phrasing()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && ! _dialogueDisplayer.dialogueStarted && _player != null)
            {
                
                _dialogueDisplayer.StartingDialogue();
                _movingDialogueBox = FindObjectOfType<MovingDialogueBox>();
                if (_movingDialogueBox == null)
                {
                    Debug.LogError("_movingDialogueBox is null");
                }
                else
                {
                    Debug.Log("Transform: " + transform);
                    _movingDialogueBox.SetCustomer(transform);
                }

            }
            if (_player != null)
                _player.ActionHandler.canMove = ! _dialogueDisplayer.dialogueStarted;

        }

        private void OrderWaiting()
        {
            var mask = LayerUtils.RightOrderLayer;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 2f, mask); // Fix this logic - we need this ray to cast in the direction of a table
            if (hit.collider != null)
            {
                Debug.Log("You did great!");
            }
        }

        private void Leaving()
        {

        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == TagUtils.PlayerTagName)
            {
                _player = collision.GetComponent<PlayerCharacter>();
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            _player = null;
        }



    }
}
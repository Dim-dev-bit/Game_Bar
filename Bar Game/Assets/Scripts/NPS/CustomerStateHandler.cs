using Assets.Scripts.UI;
using BarGame.Player;
using System;
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

        private bool IsPlayer = false;

        private PathFindingLogic _pathFindingLogic;
        private PlayerCharacter _player;

        private Rigidbody2D _rb;

        protected void Awake()
        {
            _startingState = State.Searching;
            _currentState = _startingState;

            _pathFindingLogic = GetComponent<PathFindingLogic>();

            _rb = GetComponent<Rigidbody2D>();

            _rb.gravityScale = 0;
        }

        public void HandleState(DialogueDisplayer _dialogueDisplayer)
        {
            switch (_currentState)
            {
                case State.Phrasing:
                    Phrasing(_dialogueDisplayer);
                    CheckCurrentState(_dialogueDisplayer);
                    break;
                case State.OrderWaiting:
                    Debug.Log("I was here!");
                    break;
                case State.Leaving:
                default:
                case State.Searching:
                    _pathFindingLogic.FindingSeat();
                    CheckCurrentState(_dialogueDisplayer);
                    break;
            }
        }

        private void CheckCurrentState(DialogueDisplayer _dialogueDisplayer)
        {
            if (_currentState == State.Searching && _pathFindingLogic.IsStopped)
            {
                _currentState = State.Phrasing;
                _rb.velocity = new Vector2(0f, 0f);
            }
            else if (_currentState == State.Phrasing && _dialogueDisplayer.dialogueFinished) {
                _currentState = State.OrderWaiting;
                _dialogueDisplayer.dialogueFinished = false;
            }
        }

        private void Phrasing(DialogueDisplayer _dialogueDisplayer)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && ! _dialogueDisplayer.dialogueStarted && IsPlayer)
            {
                _dialogueDisplayer.StartingDialogue();
            }
            if (_player != null)
                _player.ActionHandler.canMove = ! _dialogueDisplayer.dialogueStarted;
            Debug.Log(_dialogueDisplayer.dialogueFinished);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == TagUtils.PlayerTagName)
            {
                IsPlayer = true;
                _player = collision.GetComponent<PlayerCharacter>();
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            IsPlayer = false;
        }



    }
}
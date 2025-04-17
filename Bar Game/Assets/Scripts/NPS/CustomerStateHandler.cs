using BarGame.Items;
using BarGame.Player;
using BarGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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
        private Collider2D[] _colliders = new Collider2D[1];
        private bool _recipeMatched;

        private PathFindingLogic _pathFindingLogic;
        private DialogueDisplayer _dialogueDisplayer;
        private MovingDialogueBox _movingDialogueBox;
        private RecipesManager _recipesManager;
        private PlayerCharacter _player;

        private Rigidbody2D _rb;

        private List<string> _order;
        private char[] delimiters = { ' ', ',', '.', '!', '?', ':', ';' };
        private Dictionary<string, List<string>> _recipes;
        private GameObject _givenGlassObj;
        private Glass _givenGlass;
        protected void Awake()
        {
            _startingState = State.Searching;
            _currentState = _startingState;

            _pathFindingLogic = GetComponent<PathFindingLogic>();
            _dialogueDisplayer = FindObjectOfType<DialogueDisplayer>();
            _recipesManager = new RecipesManager();
            _recipes = _recipesManager.Recipes;

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
                    Debug.Log("was");
                    _pathFindingLogic.ExitingBar(_recipeMatched);
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
            else if (_currentState == State.Phrasing && _dialogueDisplayer.dialogueFinished && _player != null) 
            {
                _currentState = State.OrderWaiting;
                _order = GetRecipe(_dialogueDisplayer.orderPhrase);

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
                    _movingDialogueBox.SetCustomer(transform);
                }

            }
            if (_player != null)
                _player.ActionHandler.canMove = ! _dialogueDisplayer.dialogueStarted;

        }

        private void OrderWaiting()
        {
            var mask = LayerUtils.PickUpLayer;
            var radius = 3f;
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, radius, _colliders, mask);
            ; // Fix this logic - we need this ray to cast in the direction of a table
            if (_colliders[0] != null)
            {
                _givenGlassObj = _colliders[0].gameObject;
                if (_givenGlassObj != null && TagUtils.IsGlass(_givenGlassObj))
                {
                    _givenGlass = _givenGlassObj.GetComponent<Glass>();
                    _recipeMatched = CompareRecipes(_givenGlass.RecipeToMatch); 
                    Debug.Log(_recipeMatched);
                    _givenGlass.RecipeToMatch.Clear();
                    _currentState = State.Leaving;
                }
            }
        }
        private List<string> GetRecipe(string inputString)
        {
            string[] words = inputString.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (Array.Find(_recipes.Keys.ToArray(), w => w.Equals(word, StringComparison.OrdinalIgnoreCase)) != null)
                    return _recipes[word];

            }
            return null;
        }

        private bool CompareRecipes(List<string> otherRecipe)
        {
            if (_order.Count == otherRecipe.Count)
            {
                for (int i = 0; i < _order.Count; i++)
                {
                    if (string.Compare(otherRecipe[i], _order[i], StringComparison.OrdinalIgnoreCase) != 0)
                        return false;
                }
                return true;
            }
            return false;   
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
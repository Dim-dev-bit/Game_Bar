using BarGame.Items;
using BarGame.Player.Interactions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BarGame.Player.Interactions {
    public class InteractionWithObjects : MonoBehaviour {
        private StateHandler stateHandler;
        private PickUpHandler pickUpHandler;
        private ActionHandler actionHandler;

        public bool IsHold;

        public Transform holdPoint;
        public SpriteRenderer mySpriteRenderer;

        public Shaker shaker;
        public Glass glass;

        [SerializeField]
        private float _lookDistance = 1f;

        protected void Awake()
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            pickUpHandler = GetComponent<PickUpHandler>();
            actionHandler = GetComponent<ActionHandler>();
            stateHandler = GetComponent<StateHandler>();
            Debug.Log("0");
            pickUpHandler.Initialize(mySpriteRenderer, holdPoint, _lookDistance);
            actionHandler.Initialize(shaker, glass);


        }

        protected void Update()
        {
            stateHandler.HandleState(actionHandler, pickUpHandler);
        }
    }
}
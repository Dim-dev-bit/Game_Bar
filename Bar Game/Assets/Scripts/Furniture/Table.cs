﻿using BarGame.NPS;
using BarGame.Player;
using UnityEngine;

namespace BarGame.Furniture {
    public class Table : MonoBehaviour {

        [SerializeField]
        public Transform[] Positions;

        private Vector2 _size = new Vector2(0, 0);


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (player != null) {
                    player.PickUpHandler.SetCurrentTable(this);
                }
            }
            if (other.CompareTag(TagUtils.CustomerTagName))
            {
                CustomerCharacter customer = other.GetComponent<CustomerCharacter>();
                if (customer != null)
                    customer.StateHandler.SetCurrentTable(this);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (player != null)
                    player.PickUpHandler.SetCurrentTable(null);
            }
            if (other.CompareTag(TagUtils.CustomerTagName))
            {
                CustomerCharacter customer = other.GetComponent<CustomerCharacter>();
                if (customer != null)
                    customer.StateHandler.SetCurrentTable(null);
            }
        }
        public bool PlaceItem(GameObject item)
        {
            int positionIndex = -1;
            for (int i = 0; i < Positions.Length; i++)
            {
                Collider2D[] _coll = Physics2D.OverlapBoxAll(Positions[i].position, _size, 0f, LayerUtils.PickUpLayer);
                if (_coll.Length == 0 )
                {
                    positionIndex = i;
                    break;
                }
            }
            if (positionIndex < 0 || positionIndex >= Positions.Length)
            {
                return false;
            }
            item.transform.position = Positions[positionIndex].position;
            return true;

        }
    }
}
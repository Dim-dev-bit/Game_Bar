using BarGame.NPS;
using BarGame.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace BarGame.Furniture {
    public class Table : MonoBehaviour {

        [SerializeField]
        public Transform[] positions;

        private Vector2 _size = new Vector2(0, 0);
        private PlayerCharacter player;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                Debug.Log("EBTER");
                player = other.GetComponent<PlayerCharacter>();
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
                player = other.GetComponent<PlayerCharacter>();
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

        private int? FindNearestPosition(Vector2 playerPosition)
        {
            int nearestIndex = -1;
            float minDistance = Mathf.Infinity;

            for (int i = 0; i < positions.Length; i++)
            {
                if (IsPositionOccupied(positions[i].position))
                    continue;

                float distance = Vector2.Distance(playerPosition, positions[i].position);

                if (distance < minDistance)
                {
                    Debug.Log(distance);
                    Debug.Log(minDistance);
                    minDistance = distance;
                    nearestIndex = i;
                }
            }

            return nearestIndex >= 0 ? nearestIndex : null;
        }

        private bool IsPositionOccupied(Vector2 position)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(
                position,
                _size,
                0f,
                LayerUtils.PickUpLayer
            );
            Debug.Log(colliders.Length);

            return colliders.Length > 0;
        }
        public bool PlaceItem(GameObject item)
        {
            if (item == null || player == null) return false;
            Debug.Log("111");
            int? positionIndex = FindNearestPosition(player.gameObject.transform.position);
            if (!positionIndex.HasValue)
            {
                Debug.Log("444");
                return false;
            }
            Debug.Log("123");
            item.transform.position = positions[positionIndex.Value].position;
            return true;

        }
    }
}
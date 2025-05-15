using BarGame.Player;
using UnityEngine;

namespace BarGame.Items {
    public class IceGenerator : MonoBehaviour {
        [SerializeField] private GameObject IcePrefab;
        [SerializeField] private Transform IcePositionTransform;
        private float _currentTime;
        private float _maxTime;
        private bool _hasTakenIce;
        protected void Start()
        {
            _currentTime = 0f;
            _maxTime = 5f;
            _hasTakenIce = false;
        }
        protected void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _maxTime && !_hasTakenIce)
            {
                // Not just transform, because it generates it somewhere... I dunno, just use Quaternion.
                Instantiate(IcePrefab, IcePositionTransform.position, Quaternion.identity);
                _currentTime = 0f;
                _hasTakenIce = true;
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagUtils.PlayerTagName))
            {
                PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
                GameObject pickUp = player.PickUpHandler.PickUp;
                if (pickUp != null && pickUp.CompareTag(TagUtils.IceTagName) && _hasTakenIce)
                {
                    _hasTakenIce = false;
                    _currentTime = 0f;
                }
            }
        }
    }
}
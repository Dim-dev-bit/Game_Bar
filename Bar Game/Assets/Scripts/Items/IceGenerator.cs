using BarGame.Player;
using BarGame.ProgressBar;
using UnityEngine;

namespace BarGame.Items {
    public class IceGenerator : MonoBehaviour {
        [SerializeField] private GameObject IcePrefab;
        public ProgressBarForIce _progressBarForIce;
        private PlayerCharacter _player;
        private bool _hasTakenIce;
        protected void Start()
        {
            _hasTakenIce = false;
            _progressBarForIce.StartProgress();
        }
        protected void Update()
        {
            if (_player != null && _player.PickUpHandler.PickUp == null && Input.GetKeyDown(KeyCode.UpArrow) && _progressBarForIce.currentAmount != 0)
            {
                GameObject ice = Instantiate(IcePrefab, _player.PickUpHandler.holdPoint.position, Quaternion.identity);
                _player.PickUpHandler.SetCurrentPickUp(ice);
                ice.transform.SetParent(null);

                _progressBarForIce.currentAmount--;
                _progressBarForIce.UpdateProgress(_progressBarForIce.currentAmount);
            }
        }
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                Debug.Log("ENTER");
                _player = other.GetComponent<PlayerCharacter>();
            }
        }
        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagUtils.PlayerTagName))
            {
                _player = null;
            }
        }
    }
}
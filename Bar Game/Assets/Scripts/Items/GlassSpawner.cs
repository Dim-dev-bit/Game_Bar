using System.Collections;
using UnityEngine;

namespace BarGame.Items {
    public class GlassSpawner : MonoBehaviour {

        [SerializeField] GameObject glass;
        [SerializeField] Transform _outputPosition;
        private float _maxTime = 3f;
        private float _currentTime = 0;
        private int maxCount = 5;
        private int currentCount = 0;

        protected void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _maxTime && currentCount < maxCount)
            {
                GameObject gl = Instantiate(glass, new Vector3(_outputPosition.position.x, _outputPosition.position.y, 0), Quaternion.identity);
                gl.transform.SetParent(null);
                _currentTime = 0;
                currentCount++;
            }
        }
    }
}
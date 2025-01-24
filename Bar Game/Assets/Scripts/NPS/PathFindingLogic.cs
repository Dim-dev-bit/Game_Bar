using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
namespace BarGame.NPS {
    public class PathFindingLogic : MonoBehaviour {
        [SerializeField]
        private float _lookRadius = 100f;

        private GameObject _target;
        private NavMeshAgent _agent;
        private Collider2D[] _colliders = new Collider2D[2];


        protected void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        protected void Update()
        {
            if (_target == null)
            {
                _target = GetTarget();
            }
            if (_target != null)
                _agent.SetDestination(_target.transform.position);
        }


        private GameObject GetTarget()
        {
            GameObject target = null;
            var position = transform.position;
            var radius = _lookRadius;
            var mask = LayerUtils.FreeChairLayer;

            var size = Physics2D.OverlapCircleNonAlloc(position, radius, _colliders, mask);
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject && _colliders[i].gameObject.layer != LayerUtils.BusyChairLayer)
                    {
                        target = _colliders[i].gameObject;
                        target.layer = LayerUtils.BusyChairLayer;
                        break;
                    }
                }
            }

            return target;
        }
    }
}
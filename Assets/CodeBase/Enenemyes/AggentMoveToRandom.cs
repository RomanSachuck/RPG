using UnityEngine.AI;
using UnityEngine;
using System.Collections;

namespace Assets.CodeBase.Enenemyes
{
    public class AggentMoveToRandom : FollowAbstract
    {
        [SerializeField] private NavMeshAgent _agent;
        private Coroutine _moveCoroutine;

        private void OnEnable()
        {
            if(_moveCoroutine == null)
                _moveCoroutine = StartCoroutine(RandomMove());
        }

        private void OnDisable()
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }

        private IEnumerator RandomMove()
        {
            while(true)
            {
                yield return new WaitForSeconds(7);
                _agent.destination = CreateRandomDestination();
            }
        }

        private Vector3 CreateRandomDestination() =>
            new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10)); 
    }
}

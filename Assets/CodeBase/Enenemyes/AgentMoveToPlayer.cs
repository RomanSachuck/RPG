using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Services.Factory;
using UnityEngine;
using UnityEngine.AI;

public class AgentMoveToPlayer : FollowAbstract
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _minimalDistance = 3;

    private IGameFactory _gameFactory;
    private Transform _heroTransform;

    private void Awake()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();

        if (_gameFactory.HeroGameObject != null)
            InitializeHeroTransform();
        else
            _gameFactory.HeroCrated += InitializeHeroTransform;
    }

    void Update()
    {
        if (Initialized() && HeroNotReached())
            _agent.destination = _heroTransform.position;
    }

    private bool Initialized() => 
        _heroTransform != null;

    private void InitializeHeroTransform() => 
        _heroTransform = _gameFactory.HeroGameObject.transform;

    private bool HeroNotReached() => 
        Vector3.Distance(transform.position, _heroTransform.position) >= _minimalDistance;
}

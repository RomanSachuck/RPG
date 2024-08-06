using Assets.CodeBase.Enenemyes;
using Assets.CodeBase.Hero;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Services.Factory;
using System.Collections;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private FollowAbstract _followHero;
    [SerializeField] private AggentMoveToRandom _randomMove;
    [SerializeField] private float _cooldown;

    private Coroutine _cooldownCoroutine;
    private bool _heroIsDead;

    private void Start()
    {
        var factory = AllServices.Container.Single<IGameFactory>();
        factory.HeroCrated += () =>
        factory.HeroGameObject.transform.GetComponent<HeroDeath>().HeroDied += HeroDied;

        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;

        SwitchFollowOff();
    }

    private void OnDisable()
    {
        _triggerObserver.TriggerEnter -= TriggerEnter;
        _triggerObserver.TriggerExit -= TriggerExit;
    }

    private void TriggerEnter(Collider obj)
    {
        if(!_heroIsDead)
            SwitchFollowOn();

        if(_cooldownCoroutine != null)
        {
            StopCoroutine(_cooldownCoroutine);
            _cooldownCoroutine = null;
        }
    }

    private void TriggerExit(Collider collider)
    {
        if(_cooldownCoroutine == null)
            _cooldownCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown()); 
    }

    private void HeroDied()
    {
        _heroIsDead = true;
        SwitchFollowOff();
    }

    private IEnumerator SwitchFollowOffAfterCooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        SwitchFollowOff();
    }

    private void SwitchFollowOn()
    {
        _randomMove.enabled = false;
        _followHero.enabled = true;
    }

    private void SwitchFollowOff()
    {
        _followHero.enabled = false;
        _randomMove.enabled = true;
    }
}

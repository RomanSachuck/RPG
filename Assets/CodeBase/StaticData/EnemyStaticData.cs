using UnityEngine;

namespace Assets.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyType EnemyType;

        [Range(0, 100)]
        public int Hp;

        [Range(1, 50)]
        public float Damage;

        [Range(.5f, 10)]
        public float AttackDistance;

        [Range(.5f, 3)]
        public float DamageRadius;

        public GameObject Prefab; 
    }
}

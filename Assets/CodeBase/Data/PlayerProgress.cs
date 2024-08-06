using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public State HeroState;
        public Stats HeroStats;

        public PlayerProgress(string level)
        {
            WorldData = new WorldData(level);
            HeroState = new State();
            HeroStats = new Stats();
        }
    }
}

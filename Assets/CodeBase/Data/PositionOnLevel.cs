using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public Vector3Data Position;

        public PositionOnLevel(string level) => 
            Level = level;

        public PositionOnLevel(string level, Vector3Data position)
        {
            Level = level;
            Position = position;
        }
    }
}

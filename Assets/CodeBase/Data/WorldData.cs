﻿using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;

        public WorldData(string level) =>
            PositionOnLevel = new PositionOnLevel(level);
    }
}
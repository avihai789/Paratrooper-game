using System;

namespace Paratrooper.Config
{
    [Serializable]
    public class Config
    {
        public LevelData[] levelsData;

        [Serializable]
        public class LevelData
        {
            public string levelName;
            public int coinsToSpawn;
            public int coinsToCollect;
            public int time;
        }
    }
}
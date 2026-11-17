using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public string levelName = "MijnLevel";
    public List<PlacedBlockData> placedBlocks = new List<PlacedBlockData>();
}

[Serializable]
public class PlacedBlockData
{
    public string blockName;
    public float posX;
    public float posY;
}
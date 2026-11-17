using System.IO;
using UnityEngine;

public class PlaySceneLoader : MonoBehaviour
{
    public GridPlacement gridPlacement;

    void Start()
    {
        if (gridPlacement == null)
        {
            gridPlacement = Object.FindAnyObjectByType<GridPlacement>();
        }

        LoadAndSpawnTempLevel();
    }

    void LoadAndSpawnTempLevel()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Levels");
        string filePath = Path.Combine(folderPath, "temp_level.pixelquest");

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            LevelData data = JsonUtility.FromJson<LevelData>(jsonString);

            if (gridPlacement != null)
            {
                gridPlacement.ClearLevel();

                foreach (PlacedBlockData bData in data.placedBlocks)
                {
                    Vector2 pos = new Vector2(bData.posX, bData.posY);
                    gridPlacement.SpawnLoadedBlock(bData.blockName, pos);
                }

                Debug.Log("Speel-scène heeft alle level data succesvol ingeladen en gespawned!");
            }
        }
        else
        {
            Debug.LogWarning("Geen temp_level.pixelquest gevonden om te laden in LevelPlay!");
        }
    }
}
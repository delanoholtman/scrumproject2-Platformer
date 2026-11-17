using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridPlacement : MonoBehaviour
{
    public float GridSize = 1;
    public GameObject[] BlockPlace;

    [Header("UI Blocking Settings")]
    public float uiMenuWidth = 280f;
    public float uiTopHeight = 150f;

    [Header("UI Dropdown Settings")]
    public float dropdownWidth = 180f;
    public float dropdownHeight = 35f;

    private List<GameObject> spawnedBlocks = new List<GameObject>();

    void Start()
    {
        if (PlayerPrefs.GetInt("ClearLevelOnStart", 0) == 1)
        {
            PlayerPrefs.SetInt("ClearLevelOnStart", 0);
            PlayerPrefs.Save();

            string filePath = Path.Combine(Application.persistentDataPath, "Levels", "temp_level.pixelquest");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            ClearLevel();
            Debug.Log("Builder is vers gestart vanuit het Hoofdmenu en is helemaal leeg gemaakt.");
        }
        else
        {
            LoadTempLevelInBuilder();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LevelPlay")
        {
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        bool isOverLeftMenu = mousePos.x < uiMenuWidth;
        bool isOverTopBar = mousePos.y > (Screen.height - uiTopHeight);

        if (isOverLeftMenu || isOverTopBar)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlaceBlock();
        }

        if (Input.GetMouseButtonUp(1))
        {
            RemoveBlock();
        }
    }

    private void PlaceBlock()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 SnappedMousePos = new Vector2(Mathf.Round(MousePos.x / GridSize) * GridSize, Mathf.Round(MousePos.y / GridSize) * GridSize);

        int selectedIndex = GameManager.Instance.BlockPicked;
        if (selectedIndex >= 0 && selectedIndex < BlockPlace.Length)
        {
            GameObject prefabToSpawn = BlockPlace[selectedIndex];
            if (prefabToSpawn != null)
            {
                string prefabName = prefabToSpawn.name.ToLower();

                bool isPlayer = prefabName.Contains("player") || prefabName.Contains("speler") || prefabName.Contains("spawn");
                bool isFinish = prefabName.Contains("finish") || prefabName.Contains("vlag") || prefabName.Contains("endpoint");

                if (isPlayer || isFinish)
                {
                    for (int i = spawnedBlocks.Count - 1; i >= 0; i--)
                    {
                        if (spawnedBlocks[i] != null)
                        {
                            string objName = spawnedBlocks[i].name.ToLower();
                            bool matchPlayer = isPlayer && (objName.Contains("player") || objName.Contains("speler") || objName.Contains("spawn"));
                            bool matchFinish = isFinish && (objName.Contains("finish") || objName.Contains("vlag") || objName.Contains("endpoint"));

                            if (matchPlayer || matchFinish)
                            {
                                Destroy(spawnedBlocks[i]);
                                spawnedBlocks.RemoveAt(i);
                            }
                        }
                        else
                        {
                            spawnedBlocks.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    foreach (GameObject block in spawnedBlocks)
                    {
                        if (block != null && (Vector2)block.transform.position == SnappedMousePos)
                        {
                            return;
                        }
                    }
                }

                GameObject newBlock = Instantiate(prefabToSpawn, SnappedMousePos, Quaternion.identity);

                BlockIdentifier identifier = newBlock.GetComponent<BlockIdentifier>();
                if (identifier == null)
                {
                    identifier = newBlock.AddComponent<BlockIdentifier>();
                }
                identifier.blockName = prefabToSpawn.name;

                spawnedBlocks.Add(newBlock);
            }
        }
    }

    public void ClearLevel()
    {
        foreach (GameObject obj in spawnedBlocks)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedBlocks.Clear();
    }

    public GameObject SpawnLoadedBlock(string blockName, Vector2 position)
    {
        GameObject matchingPrefab = null;
        string cleanSearchName = blockName.Trim().ToLower();
        if (cleanSearchName == "vlag") cleanSearchName = "finish";

        foreach (GameObject prefab in BlockPlace)
        {
            if (prefab != null)
            {
                string cleanPrefabName = prefab.name.Trim().ToLower();
                if (cleanPrefabName == cleanSearchName || cleanPrefabName.Contains(cleanSearchName))
                {
                    matchingPrefab = prefab;
                    break;
                }
            }
        }

        if (matchingPrefab != null)
        {
            GameObject newBlock = Instantiate(matchingPrefab, position, Quaternion.identity);

            BlockIdentifier identifier = newBlock.GetComponent<BlockIdentifier>();
            if (identifier == null)
            {
                identifier = newBlock.AddComponent<BlockIdentifier>();
            }
            identifier.blockName = matchingPrefab.name;

            spawnedBlocks.Add(newBlock);
            return newBlock;
        }
        else
        {
            Debug.LogWarning("Kon prefab met naam '" + blockName + "' niet vinden in BlockPlace array!");
            return null;
        }
    }

    public List<GameObject> GetSpawnedBlocks()
    {
        List<GameObject> saveableBlocks = new List<GameObject>();
        foreach (GameObject block in spawnedBlocks)
        {
            if (block != null)
            {
                saveableBlocks.Add(block);
            }
        }
        return saveableBlocks;
    }

    void RemoveBlock()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorldPos);

        foreach (Collider2D hit in hits)
        {
            if (hit != null)
            {
                GameObject hitObj = hit.gameObject;
                GameObject targetObj = hitObj;

                if (!spawnedBlocks.Contains(targetObj) && targetObj.transform.root != null)
                {
                    targetObj = targetObj.transform.root.gameObject;
                }

                if (spawnedBlocks.Contains(targetObj))
                {
                    spawnedBlocks.Remove(targetObj);
                    Destroy(targetObj);
                    return;
                }
            }
        }
    }

    void LoadTempLevelInBuilder()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Levels");
        string filePath = Path.Combine(folderPath, "temp_level.pixelquest");

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            LevelData data = JsonUtility.FromJson<LevelData>(jsonString);

            ClearLevel();

            foreach (PlacedBlockData bData in data.placedBlocks)
            {
                Vector2 pos = new Vector2(bData.posX, bData.posY);
                SpawnLoadedBlock(bData.blockName, pos);
            }

            Debug.Log("Builder heeft het level succesvol hersteld vanuit Play-mode!");
        }
    }
}

public class BlockIdentifier : MonoBehaviour
{
    public string blockName;
}
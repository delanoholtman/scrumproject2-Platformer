using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public GridPlacement gridPlacement;
    public UIDocument uiDocument;

    private TextField levelNameInput;
    private DropdownField levelDropdown;
    private Button saveButton;
    private Button modeSwitchButton;
    private Button blocksBtn;
    private Button playerBtn;
    private Button spikeBtn;
    private VisualElement overwritePopup;
    private Button confirmOverwriteButton;
    private Button cancelOverwriteButton;

    private string pendingLevelName;

    void Start()
    {
        if (uiDocument == null) uiDocument = GetComponent<UIDocument>();

        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;

            levelNameInput = root.Q<TextField>("LevelNameInput");
            levelDropdown = root.Q<DropdownField>("LevelDropdown");
            saveButton = root.Q<Button>("SaveButton");
            modeSwitchButton = root.Q<Button>("ModeSwitchButton");

            blocksBtn = root.Q<Button>("Blocks");
            playerBtn = root.Q<Button>("Player");
            spikeBtn = root.Q<Button>("Spike");

            overwritePopup = root.Q<VisualElement>("OverwritePopup");
            confirmOverwriteButton = root.Q<Button>("ConfirmOverwriteButton");
            cancelOverwriteButton = root.Q<Button>("CancelOverwriteButton");

            if (overwritePopup != null) overwritePopup.style.display = DisplayStyle.None;

            RefreshLevelList();

            if (levelDropdown != null && gridPlacement != null)
            {
                levelDropdown.style.width = gridPlacement.dropdownWidth;
                levelDropdown.style.minWidth = gridPlacement.dropdownWidth;
                levelDropdown.style.height = gridPlacement.dropdownHeight;
            }

            if (levelDropdown != null)
            {
                levelDropdown.RegisterValueChangedCallback(evt => {
                    if (!string.IsNullOrEmpty(evt.newValue))
                    {
                        LoadLevel(evt.newValue);
                    }
                });
            }

            if (saveButton != null) saveButton.clicked += OnSaveButtonClicked;

            if (modeSwitchButton != null)
            {
                modeSwitchButton.clicked += () =>
                {
                    SaveLevelDirectly("temp_level");
                    SceneManager.LoadScene("LevelPlay");
                };
            }

            if (blocksBtn != null) blocksBtn.clicked += () => GameManager.Instance.SelectBlock(0);
            if (playerBtn != null) playerBtn.clicked += () => GameManager.Instance.SelectBlock(1);
            if (spikeBtn != null) spikeBtn.clicked += () => GameManager.Instance.SelectBlock(2);

            if (confirmOverwriteButton != null) confirmOverwriteButton.clicked += () => { ExecuteSave(pendingLevelName); CloseOverwritePopup(); };
            if (cancelOverwriteButton != null) cancelOverwriteButton.clicked += CloseOverwritePopup;
        }
    }

    private void OnSaveButtonClicked()
    {
        string levelName = levelNameInput != null ? levelNameInput.value : "MijnLevel";
        if (string.IsNullOrEmpty(levelName)) levelName = "NaamloosLevel";

        string folderPath = Path.Combine(Application.persistentDataPath, "Levels");
        string filePath = Path.Combine(folderPath, levelName + ".pixelquest");

        if (File.Exists(filePath))
        {
            pendingLevelName = levelName;
            ShowOverwritePopup();
        }
        else
        {
            ExecuteSave(levelName);
        }
    }

    public void SaveLevelDirectly(string levelName)
    {
        ExecuteSave(levelName);
    }

    private void ExecuteSave(string levelName)
    {
        LevelData data = new LevelData();
        data.levelName = levelName;

        if (gridPlacement != null)
        {
            foreach (GameObject blockObj in gridPlacement.GetSpawnedBlocks())
            {
                if (blockObj != null)
                {
                    BlockIdentifier id = blockObj.GetComponent<BlockIdentifier>();
                    if (id != null)
                    {
                        PlacedBlockData bData = new PlacedBlockData();
                        bData.blockName = id.blockName;
                        bData.posX = blockObj.transform.position.x;
                        bData.posY = blockObj.transform.position.y;
                        data.placedBlocks.Add(bData);
                    }
                }
            }
        }

        string jsonString = JsonUtility.ToJson(data, true);
        string folderPath = Path.Combine(Application.persistentDataPath, "Levels");

        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        File.WriteAllText(Path.Combine(folderPath, levelName + ".pixelquest"), jsonString);
        Debug.Log("Level succesvol opgeslagen als: " + levelName);

        RefreshLevelList();
    }

    public void RefreshLevelList()
    {
        if (levelDropdown == null) return;

        string folderPath = Path.Combine(Application.persistentDataPath, "Levels");
        if (Directory.Exists(folderPath))
        {
            var files = Directory.GetFiles(folderPath, "*.pixelquest");
            List<string> levelNames = new List<string>();
            foreach (var file in files)
            {
                string name = Path.GetFileNameWithoutExtension(file);
                if (name != "temp_level")
                {
                    levelNames.Add(name);
                }
            }
            levelDropdown.choices = levelNames;
        }
    }

    public void LoadLevel(string levelName)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Levels");
        string filePath = Path.Combine(folderPath, levelName + ".pixelquest");

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
            }

            Debug.Log("Level succesvol geladen: " + levelName);
        }
    }

    private void ShowOverwritePopup()
    {
        if (overwritePopup != null) overwritePopup.style.display = DisplayStyle.Flex;
    }

    private void CloseOverwritePopup()
    {
        if (overwritePopup != null) overwritePopup.style.display = DisplayStyle.None;
    }
}
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public GridPlacement gridPlacement;
    public GameObject playerPrefab;
    private GameObject spawnedPlayer;
    public bool IsPlayMode { get; private set; } = false;

    public void ToggleGameMode()
    {
        IsPlayMode = !IsPlayMode;

        if (IsPlayMode)
        {
            if (gridPlacement != null) gridPlacement.enabled = false;
            if (playerPrefab != null)
            {
                spawnedPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            }
            Debug.Log("Overgeschakeld naar Speelmodus.");
        }
        else
        {
            if (spawnedPlayer != null) Destroy(spawnedPlayer);
            if (gridPlacement != null) gridPlacement.enabled = true;

            Debug.Log("Overgeschakeld naar Bouwmodus.");
        }
    }
}
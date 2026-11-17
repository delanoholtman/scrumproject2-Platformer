using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SpawnPlayerAtFlagRoutine());
    }

    IEnumerator SpawnPlayerAtFlagRoutine()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject player = GameObject.FindWithTag("Player");
        GameObject flag = GameObject.FindWithTag("EndPoint");

        if (player != null && flag != null)
        {
            player.transform.position = flag.transform.position;
            Debug.Log("Speler succesvol bij de vlag geplaatst door PlayerSpawner!");
        }
        else
        {
            Debug.LogWarning("Speler of Vlag (EndPoint) kon nog niet worden gevonden in LevelPlay! Controleer of de vlag de tag 'EndPoint' heeft.");
        }
    }
}
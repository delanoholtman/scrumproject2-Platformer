using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int BlockPicked = 0;
    bool TimerOn = false;
    public float Timer = 0f;

    void FixedUpdate()
    {
        if (TimerOn)
        {
            Timer += Time.deltaTime;
            Debug.Log("timer is on");
        }
    }

    public void startTimer()
    {
        TimerOn = true;
    }
    public void stopTimer()
    {
        TimerOn = false;
        Debug.Log("trigger");
    }

    public void TimeReset()
    {
        Timer = 0f;
    }

    public void SelectBlock(int index)
    {
        BlockPicked = index;
        Debug.Log("Blok geselecteerd met index: " + index);
    }
}

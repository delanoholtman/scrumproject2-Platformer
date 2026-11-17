using UnityEngine;
using UnityEngine.UIElements;

public class UI_Editor : MonoBehaviour
{
    Button blocks;
    Button Player;
    Button Spike;
    Button Enemy1;
    Button Enemy2;
    Button Finish;
    Button Jump;
    Button Speed;
    Button Saw;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        blocks = root.Q<Button>("Blocks");
        Player = root.Q<Button>("Player");
        Spike = root.Q<Button>("Spike");
        Enemy1 = root.Q<Button>("enemy1");
        Enemy2 = root.Q<Button>("enemy2");
        Finish = root.Q<Button>("finish");
        Jump = root.Q<Button>("JumpPower");
        Speed = root.Q<Button>("SpeedPower");
        Saw = root.Q<Button>("saw");

        blocks.clicked += OnBlocksClicked;
        Player.clicked += OnPlayerClicked;
        Spike.clicked += OnSpikeClicked;
        Enemy1.clicked += OnEnemy1Clicked;
        Enemy2.clicked += OnEnemy2Clicked;
        Finish.clicked += OnFinishClicked;
        Jump.clicked += OnJumpClicked;
        Speed.clicked += OnSpeedClicked;
        Saw.clicked += OnSawClicked;
    }

    void OnBlocksClicked()
    {
        GameManager.Instance.BlockPicked = 0;
    }

    void OnPlayerClicked()
    {
        GameManager.Instance.BlockPicked = 1;
    }

    void OnSpikeClicked()
    {
        GameManager.Instance.BlockPicked = 2;
    }
    void OnEnemy1Clicked()
    {
        GameManager.Instance.BlockPicked = 3;
    }
    void OnEnemy2Clicked()
    {
        GameManager.Instance.BlockPicked = 4;
    }
    void OnFinishClicked()
    {
        GameManager.Instance.BlockPicked = 5;
    }
    void OnJumpClicked()
    {
        GameManager.Instance.BlockPicked = 6;
    }
    void OnSpeedClicked()
    {
        GameManager.Instance.BlockPicked = 7;
    }
    void OnSawClicked()
    {
        GameManager.Instance.BlockPicked = 8;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private enum ClothingLevel
    {
        Naked, NoJeans, NoTshirt, NoSweatshirt, NoJacket, NoHat, Full
    }

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int cold = 0;

    [SerializeField]
    private int wallet = 5;

    [SerializeField]
    private ClothingLevel clothes = ClothingLevel.Full;

    [SerializeField]
    private short time;
    public short Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
            if (time == 24)
            {
                time = 0;
            }
        }
    }

    public int Day { get { return time / 24 + 1; } }


    private void Awake()
    {
        int sessionsCount = FindObjectsOfType<GameState>().Length;
        if (sessionsCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        time++;
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void GameOver()
    {
        Debug.Log("Youre dead");
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField]
    private CanvasManager _canvasManager;
    [SerializeField]
    private LeaderBoardManager _boardManager;

    [SerializeField]
    float scorePerSecond;

    float secondsPerScore;

    float accumulatedTime;

    [HideInInspector]
    public int Score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
        LoadPersistantData();
        UpdateCanvas();

        secondsPerScore = 1 / scorePerSecond;
    }

    private void UpdateCanvas()
    {
        _canvasManager.SetScoreText(Score);
        _canvasManager.SetLeaderBoard(_boardManager.GetElements());
    }

    private void LoadPersistantData()
    {
        //highScore
        _boardManager.LoadLeaderboard();
    }

    private void Update()
    {
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime > secondsPerScore)
        {
            accumulatedTime -= secondsPerScore;
            Score++;
            _canvasManager.SetScoreText(Score);
        }
    }

}

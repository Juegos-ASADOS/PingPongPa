using UnityEngine;
using UnityEngine.InputSystem;

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

    int playersSpawned;

    [HideInInspector]
    public bool gameStarted = false;

    [SerializeField]
    AudioClip _mainLoop;

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
        if (!gameStarted)
            return;

        accumulatedTime += Time.deltaTime;
        if (accumulatedTime > secondsPerScore)
        {
            accumulatedTime -= secondsPerScore;
            Score++;
            _canvasManager.SetScoreText(Score);
        }
    }


    public void PlayerSpawned(PlayerInput playerInput)
    {
        playersSpawned++;

        playerInput.GetComponent<PlayerController>().Init(playersSpawned);

        if (playersSpawned == 2)
            gameStarted = true;
    }

    private void Start()
    {
        SetMusicTransition();
    }

    public void SetMusicTransition()
    {
        AudioSource ogSrc = GetComponent<AudioSource>();
        AudioSource newSrc = gameObject.AddComponent<AudioSource>();
        newSrc.playOnAwake = false;
        newSrc.clip = _mainLoop;
        newSrc.loop = true;

        double introStartTime = AudioSettings.dspTime + 0.2f;
        double sourceStartTime = introStartTime + ogSrc.clip.length;
        ogSrc.PlayScheduled(introStartTime);
        newSrc.PlayScheduled(sourceStartTime);

        Destroy(ogSrc, ogSrc.clip.length * 0.1f);
    }
}

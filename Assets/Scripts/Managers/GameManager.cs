using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    bool gameFinished = false;

    [SerializeField]
    float timeToReset;
    float resetTimer;

    AudioSource _mainAudioSource;

    [SerializeField]
    AudioClip _mainLoop;
    [SerializeField]
    AudioClip _intro;

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
        _mainAudioSource = gameObject.AddComponent<AudioSource>();
    }

    private void UpdateCanvas()
    {
        _canvasManager.SetScoreText(Score);
        _canvasManager.SetLeaderBoard(_boardManager.GetElements());
    }

    public void UpdateLeaderBoard()
    {
        _boardManager.TryToAddScore(Score);
        _canvasManager.SetLeaderBoard(_boardManager.GetElements());
    }

    private void LoadPersistantData()
    {
        //highScore
        _boardManager.LoadLeaderboard();
    }

    private void Update()
    {
        if (!gameStarted && !gameFinished)
            return;

        if (gameStarted)
        {
            accumulatedTime += Time.deltaTime;
            if (accumulatedTime > secondsPerScore)
            {
                accumulatedTime -= secondsPerScore;
                Score++;
                _canvasManager.SetScoreText(Score);
            }
        }
        else if (gameFinished)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                gameFinished = false;
                _boardManager.TryToAddScore(Score);
                Score = 0;
                UpdateCanvas();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void PlaySound(AudioClip clip, float pitchVariance)
    {
        _mainAudioSource.pitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
        _mainAudioSource.PlayOneShot(clip);
    }

    public void StopSound() { _mainAudioSource.Stop(); }


    public void PlayerSpawned(PlayerInput playerInput)
    {
        playersSpawned++;

        playerInput.GetComponent<PlayerController>().Init(playersSpawned);

        if (playersSpawned == 2)
            gameStarted = true;
    }

    public void PlayerDestroyed()
    {
        playersSpawned--;

        if (playersSpawned == 0)
        {
            gameFinished = true;
            gameStarted = false;
            resetTimer = timeToReset;
        }
    }

    private void Start()
    {
        SetMusicTransition();
    }

    public void SetMusicTransition()
    {
        AudioSource ogSrc = gameObject.AddComponent<AudioSource>();
        AudioSource newSrc = gameObject.AddComponent<AudioSource>();
        ogSrc.playOnAwake = false;
        newSrc.playOnAwake = false;
        ogSrc.clip = _intro;
        newSrc.clip = _mainLoop;
        newSrc.loop = true;

        double introStartTime = AudioSettings.dspTime + 0.2f;
        double sourceStartTime = introStartTime + ogSrc.clip.length;
        ogSrc.PlayScheduled(introStartTime);
        newSrc.PlayScheduled(sourceStartTime);

        Destroy(ogSrc, ogSrc.clip.length + 0.1f);
    }
}

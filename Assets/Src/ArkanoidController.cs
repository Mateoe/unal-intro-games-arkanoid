using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkanoidController : MonoBehaviour
{

    [SerializeField]
    private GridController _gridController;

    [Space(20)]
    [SerializeField]
    private List<LevelData> _levels = new List<LevelData>();

    private const string BALL_PREFAB_PATH = "Prefabs/Ball";
    private readonly Vector2 BALL_INIT_POSITION = new Vector2(0, -0.86f);
    private Ball _ballPrefab = null;
    private List<Ball> _balls = new List<Ball>();
    private int _totalScore = 0;

    private int _currentLevel = 0;
    [SerializeField]
    private Paddle _paddle;
    [SerializeField]
    private PowerUp _powerUp;

    private void Start()
    {
        ArkanoidEvent.OnBallReachDeadZoneEvent += OnBallReachDeadZone;
        ArkanoidEvent.OnBlockDestroyedEvent += OnBlockDestroyed;
    }

    private void OnDestroy()
    {
        ArkanoidEvent.OnBallReachDeadZoneEvent -= OnBallReachDeadZone;
        ArkanoidEvent.OnBlockDestroyedEvent -= OnBlockDestroyed;
    }

    private void OnBallReachDeadZone(Ball ball)
    {
        ball.Hide();
        _balls.Remove(ball);
        Destroy(ball.gameObject);

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        //Game over
        if (_balls.Count == 0)
        {
            ClearBalls();
            _powerUp.ClearPowerUps();
            Debug.Log("Game Over: LOSE!!!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitGame();
        }
    }

    private void InitGame()
    {
        _currentLevel = 0;
        _totalScore = 0;
        ArkanoidEvent.OnScoreUpdatedEvent?.Invoke(0, 0);
        _gridController.BuildGrid(_levels[0]);
        ClearBalls();
        _powerUp.ClearPowerUps();
        SetInitialBall();
    }

    private void SetInitialBall()
    {
        Ball ball = CreateBallAt(BALL_INIT_POSITION);
        ball.Init();
        _balls.Add(ball);
    }

    private Ball CreateBallAt(Vector2 position)
    {
        if (_ballPrefab == null)
        {
            _ballPrefab = Resources.Load<Ball>(BALL_PREFAB_PATH);
        }
        return Instantiate(_ballPrefab, position, Quaternion.identity);
    }

    private void ClearBalls()
    {
        for (int i = _balls.Count - 1; i >= 0; i--)
        {
            _balls[i].gameObject.SetActive(false);
            Destroy(_balls[i]);
        }
    
        _balls.Clear();
    }

    private void OnBlockDestroyed(int blockId)
    {
        BlockTile blockDestroyed = _gridController.GetBlockBy(blockId);
        if (blockDestroyed != null)
        {
            _totalScore += blockDestroyed.Score;
            ArkanoidEvent.OnScoreUpdatedEvent?.Invoke(blockDestroyed.Score, _totalScore);
        }

        if (_gridController.GetBlocksActive() == 0)
        {
            _currentLevel++;
            if (_currentLevel >= _levels.Count)
            {
                ClearBalls();
                Debug.LogError("Game Over: WIN!!!!");
            }
            else
            {
                SetInitialBall();
                _gridController.BuildGrid(_levels[_currentLevel]);
            }

        }
        if (Random.value <= 0.5f) {
            _powerUp.CreatePowerUp(blockDestroyed.transform.position);
        }
    }
    public void SumPoints(int points) 
    {
        _totalScore += points;
        ArkanoidEvent.OnScoreUpdatedEvent?.Invoke(points, _totalScore);
    } 

    public void Fast() 
    {
        for (int i = 0; i < _balls.Count; i++) {
            _balls[i].Fast();
        }
    }

    public void Slow() 
    {
        for (int i = 0; i < _balls.Count; i++) {
            _balls[i].Slow();
        }
    }

    public void Small() 
    {
        if (_paddle.transform.localScale.x > 0.4) {
            Vector3 changeSize = new Vector3(0.2f, 0f, 0f);
            _paddle.transform.localScale -= changeSize;
        }
    }

    public void Big() 
    {
        if (_paddle.transform.localScale.x < 3) {
            Vector3 changeSize = new Vector3(0.2f, 0f, 0f);
            _paddle.transform.localScale += changeSize;
        }
    }

}

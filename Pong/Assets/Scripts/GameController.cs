using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    [SerializeField] private UIManager uiManager;

    [SerializeField] private int scoreToWin = 2;
    [SerializeField] private int leftScore;
    [SerializeField] private int rightScore;

    [SerializeField] private bool inMenu;

    private Ball ball;

    [SerializeField] private Paddle leftPaddle;
    [SerializeField] private Paddle rightPaddle;

    private Paddle.Side serveSide;


    private void Awake()
    {
        instance = this;
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();

        DoMenu();
    }

    private void DoMenu()
    {
        inMenu = true;
        leftPaddle.isAI = rightPaddle.isAI = true;

        leftScore = rightScore = 0;
        uiManager.UpdateScoreText(leftScore, rightScore);
        ball.gameObject.SetActive(true);  
        ResetGame();    
    }

    public void Score(Paddle.Side side)
    {
        if(side == Paddle.Side.Left)
        {
            leftScore++;
        }
        else if(side == Paddle.Side.Right)
        {
            rightScore++;
        }

        uiManager.UpdateScoreText(leftScore, rightScore);
        serveSide = side;

        if (IsGameOver())
        {
            if (inMenu)
            {
                ResetGame();
                leftScore = rightScore = 0;
            }

            else
            {
                ball.gameObject.SetActive(false);
                uiManager.ShowGameOver(side);
            }
                
        }
        else
        {
            ResetGame();
        }

    }

    private bool IsGameOver()
    {
        bool result = false;

        if (leftScore >= scoreToWin || rightScore >= scoreToWin)
            result = true;

        return result;
    }

    private void ResetGame()
    {
        ball.gameObject.SetActive(true);
        ball.Reset(serveSide);
        leftPaddle.Reset();
        rightPaddle.Reset();
    }

    #region UI Methods
    public void StartOnePlayer()
    {
        leftPaddle.isAI = false;
        rightPaddle.isAI = true;
        InitializeGame();
    }

    public void StartTwoPlayers()
    {
        leftPaddle.isAI = false;
        rightPaddle.isAI = false;
        InitializeGame();
    }

    public void Restart()
    {
        InitializeGame();
        uiManager.OnGameStart();    
    }

    public void GoToMenu()
    {
        uiManager.ShowMenu();
        DoMenu();
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void InitializeGame()
    {
        inMenu = false;
        leftScore = rightScore = 0;
        uiManager.UpdateScoreText(leftScore, rightScore);
        ResetGame();
        uiManager.OnGameStart();
    }
    #endregion

    
}

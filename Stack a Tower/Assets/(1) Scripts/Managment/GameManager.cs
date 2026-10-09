using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;


    [SerializeField] TMP_Text scoreText;

    [SerializeField] float spawnMax;
    [SerializeField] float spawnMin;

    [SerializeField] GameObject squarePrefab;
    GameObject currentSquare;


    InputActions inputActions;

    public static int Score;

    private void Awake()
    {
        Score = 0;
        GameManagerInstanceChek();
        inputActions = new InputActions();
        SpawnNewSquare();
        UpdateScoreText();
    }

    private void OnEnable()
    {
        inputActions.GameInputs.Enable();

        inputActions.GameInputs.Place.performed += ctx => Place();
    }
    private void OnDisable()
    {
        inputActions.GameInputs.Disable();

        inputActions.GameInputs.Place.performed -= ctx => Place();
    }
    public void AddScore(int score)
    {
        Score += score;
        UpdateScoreText();
    }

    void UpdateScoreText() 
    {
        if (scoreText != null)
        {
            scoreText.text = ("Score:" + Score);
        }
    }


    void GameManagerInstanceChek()
    {
        if (gameManagerInstance == null) { gameManagerInstance = this; }
        else if (gameManagerInstance != this) { Destroy(this); }
    }

    public void SpawnNewSquare()
    {
        if (squarePrefab != null)
        {
            float xValue = Random.Range(spawnMin, spawnMax);
            if (Random.value < 0.5)
            {
                xValue = -xValue;
            }
            Vector3 spawnPos = new Vector3(xValue, 0f, 0f);
            currentSquare = Instantiate(squarePrefab, spawnPos, Quaternion.identity);
        }
    }

    void Place()
    {
        if (currentSquare != null)
        {
            currentSquare.GetComponent<SquareScript>().Place();
        }
    }
}

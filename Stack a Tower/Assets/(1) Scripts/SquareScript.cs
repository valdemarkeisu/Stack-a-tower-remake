using UnityEngine;

public class SquareScript : MonoBehaviour
{
    [SerializeField] float startSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float speedPerBounc;

    [SerializeField] int value;

    [SerializeField] float border;


    [SerializeField] float currentSpeed;
    Vector2 direction;



    private void Awake()
    {
        OnSpawn();
    }

    private void Update()
    {
        ChekForBounc();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Place()
    {
        GameManager.gameManagerInstance.AddScore(value);
        Destroy(gameObject);
    }


    void Move()
    {
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    void OnSpawn()
    {
        currentSpeed = startSpeed;
        if(transform.position.x >= 0)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
    }

    void OnBounc()
    {
        if (currentSpeed < maxSpeed)
        {
            if (currentSpeed + speedPerBounc >= maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            else
            {
                currentSpeed += speedPerBounc;
            }
        }
    }

    void ChekForBounc()
    {
        if (transform.position.x <= -border && direction != Vector2.right) { direction = Vector2.right; OnBounc();}
        if (transform.position.x >= border && direction != Vector2.left) { direction = Vector2.left; OnBounc();}
    }
}

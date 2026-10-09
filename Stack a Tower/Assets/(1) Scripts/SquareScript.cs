using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SquareScript : MonoBehaviour
{
    [SerializeField] float startSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float trueCap;
    [SerializeField] float speedPerBounc;

    [SerializeField] int value;

    [SerializeField] float border;


    [SerializeField] float currentSpeed;
    Vector2 direction;

    [SerializeField] Material goodRange;
    [SerializeField] Material baseMaterial;

    GameObject pedestal;



    private void Awake()
    {
        OnSpawn();
    }

    private void Update()
    {
        ChekForBounc();
        ChangeMaterial();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Place()
    {
        if (Mathf.Abs(transform.position.x) <= Mathf.Abs(pedestal.transform.localScale.x/2 + (4f * currentSpeed * Time.deltaTime)))
        {
            Vector3 pedestalNewScale = pedestal.transform.localScale;
            pedestalNewScale.x = Mathf.Clamp(pedestal.transform.localScale.x -(Mathf.Abs(transform.position.x)) + 0.2f,0.5f,2);
            pedestal.transform.localScale = pedestalNewScale;
            GameManager.gameManagerInstance.AddScore(value);
            GameManager.gameManagerInstance.SpawnNewSquare();
            Destroy(gameObject);
        }
        else
        {
            currentSpeed = 0;
            GameManager.gameManagerInstance.gameObject.SetActive(false);
            Debug.Log("lost");
            Destroy(gameObject);
        }
    }


    void Move()
    {
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    void OnSpawn()
    {
        pedestal = GameObject.Find("Pedestal");
        Vector3 newScale = transform.localScale;
        newScale.x = pedestal.transform.localScale.x;
        transform.localScale = newScale;
        maxSpeed = (maxSpeed + Mathf.Clamp(GameManager.Score / 5, maxSpeed, trueCap - maxSpeed));
        startSpeed = (startSpeed + Mathf.Clamp(GameManager.Score / 5, startSpeed, trueCap - startSpeed));
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

    void ChangeMaterial()
    {
        if (Mathf.Abs(transform.position.x) !< Mathf.Abs(pedestal.transform.localScale.x / 2 + 0.2f + (4f * currentSpeed * Time.deltaTime)))
        {
            GetComponent<SpriteRenderer>().material = goodRange;
        }
        else
        {
            GetComponent<SpriteRenderer>().material = baseMaterial;

        }
    }
}

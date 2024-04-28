using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Rigidbody targetRB;
    private GameManager gameManager;
    public ParticleSystem effect;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float yRange = -2;
    public int value;

    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRB.AddForce(RandomForce(),ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(),ForceMode.Impulse);
        
        transform.position = SpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up*Random.Range(minSpeed,maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque,maxTorque);
    }

    Vector3 SpawnPosition()
    {
        return new Vector3 (Random.Range(-xRange,xRange),yRange,0);
    }

    void OnMouseDown()
    {
        if(gameManager.isGameActive)
        
        {
            Destroy(gameObject);
            gameManager.UpdateScore(value);
            Instantiate(effect,transform.position,effect.transform.rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!gameObject.CompareTag("Bad") && other.CompareTag("Sensor"))
        {
            Destroy(gameObject);
            gameManager.UpdateLives();
        }
    }
}

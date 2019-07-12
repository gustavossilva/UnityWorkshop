using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaclesGenerator : MonoBehaviour
{
    public List<GameObject> obstacles = new List<GameObject>();
    public float time;
    public float timeToNextObstacle;
    private Dictionary<string, ObstacleTypes> obstaclesConfig = new Dictionary<string, ObstacleTypes>();
    // Start is called before the first frame update
    void Awake() {
        obstaclesConfig.Add("small_cac", new ObstacleTypes(4,120,0f));
        obstaclesConfig.Add("large_cac", new ObstacleTypes(7, 120, 0f));
        obstaclesConfig.Add("ptero", new ObstacleTypes(999,150,8.5f ));
    }

    // Update is called once per frame
    void Update()
    {
        this.timeToNextObstacle -= Time.deltaTime;
        if(this.timeToNextObstacle <= 0 && GameManager.instance.hasStarted) {
            GameObject nextObstacle = this.obstacles[Random.Range(0, obstacles.Count - 1)];
            Vector3 newPosition = new Vector3();
            if(nextObstacle.name.Contains("Bird1")) {
                if(Random.Range(0, 100) > 50) {
                    newPosition = new Vector3(nextObstacle.transform.position.x, -0.360f, nextObstacle.transform.position.z);
                }
            } else {
                newPosition = nextObstacle.transform.position;
            }
            GameObject next = GameObject.Instantiate(nextObstacle) as GameObject;
            next.transform.position = newPosition;
            timeToNextObstacle = time;
        }
    }
}

public class ObstacleTypes {
    public int multipleSpeed;
    public int minGap;
    public float minSpeed;
    public ObstacleTypes(int multiSpeed,int  minGap,float minSpeed) {
        this.multipleSpeed = multiSpeed;
        this.minGap = minGap;
        this.minSpeed = minSpeed;
    }
}

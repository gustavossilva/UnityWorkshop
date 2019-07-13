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
        if(GameManager.instance.hasStarted) {
            Debug.Log("Elipsed Time " + timeToNextObstacle);
            this.timeToNextObstacle -= Time.deltaTime;
            if(this.timeToNextObstacle <= 0) {
                this.timeToNextObstacle = time;
                GameObject nextObstacle = this.obstacles[Random.Range(0, obstacles.Count - 1)];
                GameObject.Instantiate(nextObstacle);
            }
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

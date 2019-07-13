using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 1f;
    public float initialPos;
    public float finalPos;
    public float yMax;
    public float yMin;

    // Update is called once per frame
    void Start() {
        float y = yMin;
        if(Random.Range(0,100) > 50) {
            y = yMax;
        }
        transform.position = new Vector2(initialPos, y);
    }
    void Update()
    {
        if (!GameManager.instance.hasStarted) {
            return;
        }
        transform.position += Vector3.left * (speed * Time.deltaTime);
        if(transform.position.x < finalPos) {
            Destroy(gameObject);
            // transform.position = new Vector2(initialPos, Random.Range(yMin, yMax));
        }
    }
}

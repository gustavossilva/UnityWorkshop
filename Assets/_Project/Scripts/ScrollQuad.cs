using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScrollQuad : MonoBehaviour {

    public float speed;
    private Material quadMaterial;
    private Vector2 offset;

	// Use this for initialization
	void Awake () 
    {
        quadMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(quadMaterial.mainTextureOffset.x, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.hasStarted) {
            return;
        }
        offset.x += Time.deltaTime * speed;
        offset.x %= 1;
        quadMaterial.mainTextureOffset = offset;
    }
}

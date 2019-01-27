using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float lerpAmount = 0.1f;
    public GameObject currentZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ratioHeight = currentZone.GetComponent<SpriteRenderer>().bounds.size.y / 9f;
        float ratioWidth = currentZone.GetComponent<SpriteRenderer>().bounds.size.x / 16f;
        GetComponent<Transform>().position = Vector2.Lerp(GetComponent<Transform>().position, currentZone.GetComponent<Transform>().position, lerpAmount);
        float newSize = (ratioHeight > ratioWidth ? ratioHeight * 9 : ratioWidth * 9);
        GetComponent<UnityEngine.Camera>().orthographicSize = Mathf.Lerp(GetComponent<UnityEngine.Camera>().orthographicSize, newSize/2, lerpAmount);
    }
}

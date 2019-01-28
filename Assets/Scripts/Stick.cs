using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Collider2D piper_collider;
    public bool collectable;
    Color dark_brown;
    Color light_brown;
    // Start is called before the first frame update
    void Start()
    {
        collectable = false;
        light_brown = new Color(0.56f, 0.411f, 0.29f);
        dark_brown = new Color(0.329f, 0.203f, 0.105f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Become_Collectable()
    {
        collectable = true;
        Debug.Log("I, the stick, am now collectable");
        GetComponent<SpriteRenderer>().color = light_brown;
    }

    void Become_Uncollectable()
    {
        collectable = false;
        Debug.Log("I, the stick, am no longer collectable");
        GetComponent<SpriteRenderer>().color = dark_brown;
    }
}

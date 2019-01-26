using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Collider2D piper_collider;
    public bool collectable;
    // Start is called before the first frame update
    void Start()
    {
        collectable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Become_Collectable()
    {
        collectable = true;
        Debug.Log("I, the stick, am now collectable");
    }

    void Become_Uncollectable()
    {
        collectable = false;
        Debug.Log("I, the stick, am no longer collectable");
    }
}

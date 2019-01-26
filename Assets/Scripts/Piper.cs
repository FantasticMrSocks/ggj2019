using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piper : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Piper loaded");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stick")
        {
            Debug.Log("Yep, that's a stick");
            collision.gameObject.SendMessage("Become_Collectable");
        }

        //if (collision.relativeVelocity.magnitude > 2)
        //    audioSource.Play();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stick")
        {
            collision.gameObject.SendMessage("Become_Uncollectable");
        }

        //if (collision.relativeVelocity.magnitude > 2)
        //    audioSource.Play();
    }
}

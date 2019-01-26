using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<GameObject> collected_sticks;
    GameObject[] found_sticks;
    // Start is called before the first frame update
    void Start()
    {
        collected_sticks = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            found_sticks = GameObject.FindGameObjectsWithTag("Stick");
            Vector3 offscreen = new Vector3(-1000f, -1000f, 0f);
            foreach (GameObject s in found_sticks)
            {
                if (s.GetComponent<Stick>().collectable)
                {
                    collected_sticks.Add(s);
                    s.transform.position = offscreen;
                    s.SetActive(false);
                }
            }
        }
    }
}

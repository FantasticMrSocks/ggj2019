using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Stack<GameObject> collected_sticks;
    GameObject[] found_sticks;

    [HideInInspector] public bool in_build_mode = false;
    GameObject current_placing_stick;
    List<GameObject> placed_sticks;
    // Start is called before the first frame update
    void Start()
    {
        collected_sticks = new Stack<GameObject>();
        placed_sticks = new List<GameObject>();
    }

    void PrepareStickForPlacement()
    {
        if (collected_sticks.Count > 0)
        {
            current_placing_stick = collected_sticks.Pop();
            current_placing_stick.SetActive(true);
            current_placing_stick.GetComponent<Collider2D>().enabled = false;
            current_placing_stick.GetComponent<Rigidbody2D>().gravityScale = 0;
            current_placing_stick.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }
        else
        {
            current_placing_stick = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(in_build_mode)
        {
            if(current_placing_stick != null)
            {
                current_placing_stick.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                var d = Input.GetAxis("Mouse ScrollWheel");
                current_placing_stick.transform.Rotate(0f, 0f, d * 20);
                if(Input.GetMouseButtonDown(1))
                {
                    placed_sticks.Add(current_placing_stick);
                    PrepareStickForPlacement();
                }
            }
            //Mouse rotate? Rotate the current stick

            // Right click? Place the current stick in Physics Off mode
        }


        if (Input.GetKeyDown("b"))
        {
            if (in_build_mode)
            {
                //Exit build mode:
                // If we have an uplaced stick on the mouse, but it back in the inventory
                if (current_placing_stick != null)
                {
                    current_placing_stick.SetActive(false);
                    collected_sticks.Push(current_placing_stick);
                    current_placing_stick = null;
                }
                // Turn on all the placed sticks
                foreach(GameObject s in placed_sticks)
                {
                    s.GetComponent<Collider2D>().enabled = true;
                    s.GetComponent<Rigidbody2D>().gravityScale = 1;
                }
                placed_sticks.Clear();
            }
            else
            {
                //Enter build mode:
                // If we have a stick, spawn it at the mouse
                
                    Debug.Log("Entered build mode, placing stick at curosr");
                    PrepareStickForPlacement();
            }

            in_build_mode = !in_build_mode;
            SendMessage("SetBuildMode", in_build_mode);
        }
        else if (Input.GetKeyDown("f") && !in_build_mode)
        {
            found_sticks = GameObject.FindGameObjectsWithTag("Stick");
            Vector3 offscreen = new Vector3(-1000f, -1000f, 0f);
            foreach (GameObject s in found_sticks)
            {
                if (s.GetComponent<Stick>().collectable)
                {
                    collected_sticks.Push(s);
                    s.transform.position = offscreen;
                    s.SetActive(false);
                }
            }
        }
    }
}

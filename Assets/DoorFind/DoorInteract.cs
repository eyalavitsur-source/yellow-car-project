using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour
{
    Camera camera;
    public GameObject DoorPrompt = null;
    public GameObject Door = null;
    public GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        DoorPrompt = GameObject.Find("DoorPrompt");
        player = GameObject.Find("firstPersonPlayer");
        DoorPrompt.SetActive(false);
        //DoorPrompt.GetComponent<TMP_Text>().text = "Escape(E)";


    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 2f))
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 2f))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SceneManager.LoadScene("Player 2 win");
                        Debug.Log("pressed");
                        return;
                    }

                    DoorPrompt.SetActive(true);
                    return;
                }
            }
        }
        DoorPrompt.SetActive(false);
    }
}

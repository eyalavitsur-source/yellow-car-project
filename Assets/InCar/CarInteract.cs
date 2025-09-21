using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarInteract : MonoBehaviour
{
    Camera camera;
    public GameObject CarPrompt = null;
    public GameObject CarPrefab = null;
    public GameObject player = null;
    public GameObject myDoor;

    void Start()
    {
        camera = Camera.main;
        CarPrompt = GameObject.Find("CarPrompt");
        player = GameObject.Find("firstPersonPlayer");
        CarPrompt.SetActive(false);
        CarPrompt.GetComponent<TMP_Text>().text = "Enter car(E)";
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 5f))
        {
            if (hit.transform.root.transform == transform)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Remove "in car" props
                    RemoveInCar[] foundScripts = FindObjectsOfType<RemoveInCar>();
                    foreach (RemoveInCar script in foundScripts)
                        Destroy(script.gameObject);

                    // 🔴 Change all lamps
                    Renderer[] allRenderers = FindObjectsOfType<Renderer>();
                    foreach (Renderer rend in allRenderers)
                    {
                        if (rend.name.ToLower().Contains("light"))
                        {
                            // Mesh color
                            rend.material.color = Color.red;
                            rend.material.SetColor("_EmissionColor", Color.red * 4f);

                            // Light component
                            Light lampLight = rend.GetComponent<Light>();
                            if (lampLight == null)
                                lampLight = rend.GetComponentInChildren<Light>();

                            if (lampLight != null)
                            {
                                lampLight.color = Color.red;
                                lampLight.intensity *= 1.5f;
                                lampLight.range *= 1.5f;
                                if (lampLight.type == LightType.Spot)
                                    lampLight.spotAngle = 100f;

                                // Add flicker component
                                LampFlicker flicker = lampLight.GetComponent<LampFlicker>();
                                if (flicker == null)
                                    flicker = lampLight.gameObject.AddComponent<LampFlicker>();

                                flicker.StartBlinking(1f);
                            }
                        }
                    }

                    Debug.Log("pressed");
                    CarPrefab.SetActive(true);
                    CarPrompt.SetActive(false);

                    playerMovementScript.isCar = true;
                    player.transform.position = transform.position + Vector3.up * 3 + transform.forward * -4;
                    player.transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
                    camera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    Rigidbody rb = player.AddComponent<Rigidbody>();
                    rb.mass = 100;
                    rb.useGravity = true;
                    rb.constraints |= RigidbodyConstraints.FreezeRotationX;
                    player.GetComponent<playerMovementScript>().rb = rb;
                    player.GetComponent<playerMovementScript>().controller.enabled = false;
                    myDoor = GameObject.Find("myDoor");
                    Destroy(gameObject);
                    ExitWall();

                    return;
                }

                CarPrompt.SetActive(true);
                return;
            }
        }
        CarPrompt.SetActive(false);
    }

    public void ExitWall()
    {
        float z = 0;
        float x = 0;
        float y = 3;
        float w = 0;
        int wall = Random.Range(1, 5);
        switch (wall)
        {
            case 1:
                z = -58;
                x = Random.Range(-446, -184);
                break;
            case 2:
                x = -448;
                z = Random.Range(-55, 368);
                w = 90;
                break;
            case 3:
                z = 371;
                x = Random.Range(-446, -184);
                w = 180;
                break;
            case 4:
                x = -182;
                z = Random.Range(-55, 368);
                w = 270;
                break;
        }
        myDoor.transform.position = new Vector3(x, y, z);
        myDoor.transform.rotation = Quaternion.Euler(0, w, 0);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCarManager : MonoBehaviour
{
    public GameObject CarView;
    [SerializeField] GameObject yellowCarPrefab;

    void Awake()
    {
        StartCoroutine(ReplaceOneWithYellow());
    }

    IEnumerator ReplaceOneWithYellow()
    {
        yield return new WaitForSeconds(0.5f); // Give time for all RandomCar.Start() to run

        List<RandomCar> allCars = RandomCar.allCars;
        Debug.Log("YellowCarManager: Found " + allCars.Count + " car spawners.");

        if (allCars.Count == 0)
        {
            Debug.LogWarning("No cars found to replace with yellow.");
            yield break;
        }

        int index = Random.Range(0, allCars.Count);
        index = 3;
        RandomCar chosen = allCars[index];
        Debug.Log("Replacing car at: " + chosen.gameObject.name);

        if (chosen.spawnedCar != null)
        {
            Destroy(chosen.spawnedCar);
        }

        Instantiate(yellowCarPrefab, chosen.transform.position, chosen.transform.rotation).GetComponent<CarInteract>().CarPrefab = CarView;
    }
}
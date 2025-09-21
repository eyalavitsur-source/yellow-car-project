using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCar : MonoBehaviour
{
    private static bool YellowGen = false;
    [SerializeField] GameObject yellowCar;
    [SerializeField] GameObject RedCar;
    [SerializeField] GameObject BlueCar;
    [SerializeField] GameObject GreenCar;
    [SerializeField] GameObject BlackCar;
    [SerializeField] GameObject WhiteCar;
    [SerializeField] GameObject BrownCar;
    [HideInInspector] public int chosen;
    [HideInInspector] public GameObject spawnedCar;
    public static List<RandomCar> allCars = new List<RandomCar>();
    // Start is called before the first frame update
    void Start()
    {
        allCars.Add(this);
        System.Random random = new System.Random();
        int num = random.Next(1, 17);
        chosen = num;
        if (num == 1)
        {
            spawnedCar = Instantiate(RedCar, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        }
        else if (num == 2)
        {
            spawnedCar = Instantiate(BlackCar, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        }
        else if(num == 3)
        {
            spawnedCar = Instantiate(BlueCar, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        }
        else if (num == 4)
        {
            spawnedCar = Instantiate(GreenCar, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        }
        else if (num == 5)
        {
            spawnedCar = Instantiate(BrownCar, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        }
        else if (num == 6)
        {
            spawnedCar = Instantiate(WhiteCar, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        }

    }
}

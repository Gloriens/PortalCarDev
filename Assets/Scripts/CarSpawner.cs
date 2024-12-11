using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject gold;
    public GameObject carPrefab;
    public GameObject carPrefab2;
    public GameObject carPrefab3;
    public GameObject carPrefab4;
    public GameObject carPrefab5;
    
    private GameObject[] spawns = new GameObject[5];
    private Transform[] spawnPoints = new Transform[4];
    
    private float spawnInterval = 2.3f; 
    private float minSpawnInterval = 1f; 
    private float spawnDecreaseRate = 0.1f;
    
    private float avarageCarSpeed = 22f;
    private float carSpeed;
    private int carPosition;
   
    
    
    private int rangeForSecondCar = 50;
    private int rangeForThirdCar = 20;
    
    void Start()
    {
        spawns[0] = carPrefab;
        spawns[1] = carPrefab2;
        spawns[2] = carPrefab3;
        spawns[3] = carPrefab4;
        spawns[4] = carPrefab5;
        spawnPoints[0] = gameObject.transform.GetChild(0).transform;
        spawnPoints[1] = gameObject.transform.GetChild(1).transform;
        spawnPoints[2] = gameObject.transform.GetChild(2).transform;
        spawnPoints[3] = gameObject.transform.GetChild(3).transform;

        StartCoroutine(SpawnCar(spawnInterval));
        StartCoroutine(MultiCarRateChanger());
        StartCoroutine(carSpeedCalculator());
    }

    private void Update()
    {
        float x = player.transform.position.x - 310;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    IEnumerator SpawnCar(float time)
    {
        while (true)
        {
            GameObject chosenCar = getRandomCar();
            Transform spawnPoint = getRandomPosition();
            GameObject spawnedObject = Instantiate(chosenCar, spawnPoint.position, chosenCar.transform.rotation);

            // Gold objesi değilse MoveCar başlat
            if (chosenCar != gold)
            {
                StartCoroutine(MoveCar(spawnedObject, getRandomSpeed()));
                
            }
            else
            {
                Vector3 adjustedPosition = spawnedObject.transform.position;
                adjustedPosition.y += 2;
                spawnedObject.transform.position = adjustedPosition;
                
                float temp = -getRandomSpeed();
                StartCoroutine(MoveCar(spawnedObject, 0));
                StartCoroutine(AnimateGold(spawnedObject));
            }

            Destroy(spawnedObject, 10);

            StartCoroutine(multiCarSpawner());

            time = Mathf.Max(minSpawnInterval, time - spawnDecreaseRate);

            yield return new WaitForSeconds(time);
        }
    }


    private IEnumerator MoveCar(GameObject car, float speed)
    {
        while (car != null)
        {
            car.transform.Translate(-car.transform.right * speed * Time.deltaTime);
            yield return null;
        }
    }

    private Transform getRandomPosition()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        carPosition = randomIndex; 
        return spawnPoints[randomIndex];
    }

    private GameObject getRandomCar()
    {
        
        int goldOrCar= Random.Range(0, 100);
        if (goldOrCar < 80)
        {
            int randomIndex = Random.Range(0, spawns.Length);
            return spawns[randomIndex];
        }
        else
        {
            Debug.Log("Gold Spawn oldu");
            return gold;
        }
        
    }

    private IEnumerator multiCarSpawner()
    {
        int carNumberChance = Random.Range(0, 100);
        
        Debug.Log("ANALİZ" + "\nAtılan zar: " + carNumberChance);
        Debug.Log("Minimum gerekn zar:   2. araç için: " + rangeForSecondCar + "    3. araç için: " + rangeForThirdCar);
        if (carNumberChance < rangeForSecondCar)
        {
            Debug.Log("İkinci araç spawn oldu");
            
            float delay = Random.Range(0f, 3f);
            yield return new WaitForSeconds(delay);
            
            
            GameObject chosenCar2 = getRandomCar();
           
            
            int randomIndex2 = Random.Range(0, spawnPoints.Length);
            
            while (randomIndex2 == carPosition)
            {
                randomIndex2 = Random.Range(0, spawnPoints.Length);
            }
            

            GameObject spawnedCar2 = Instantiate(chosenCar2, spawnPoints[randomIndex2].position, chosenCar2.transform.rotation);
            StartCoroutine(MoveCar(spawnedCar2, getRandomSpeed()));
            Destroy(spawnedCar2, 10);

            if (carNumberChance < rangeForThirdCar)
            {
                
                GameObject chosenCar3 = getRandomCar();  
                Debug.Log("3. araba spawn olacak: " + chosenCar3.name + " Spawn olma ihtimali: " + carNumberChance + " Atılan zar: " + carNumberChance);
                
                int randomIndex3 = Random.Range(0, spawnPoints.Length); 
                while (randomIndex2 == randomIndex3)                   
                {                                                      
                    randomIndex3 = Random.Range(0, spawnPoints.Length);
                }      
                
                float delay2 = Random.Range(0f, 3f);                                                                                
                yield return new WaitForSeconds(delay2);                                                                            
                                                                                                                    
                GameObject spawnedCar3 = Instantiate(chosenCar3, spawnPoints[randomIndex3].position, chosenCar3.transform.rotation); 
                StartCoroutine(MoveCar(spawnedCar3, getRandomSpeed()));          
                Destroy(spawnedCar3, 10);                                        
            }
            
            
            
            
        }
    }

    private IEnumerator MultiCarRateChanger()
    {
        while (true)
        {
            rangeForSecondCar = Mathf.Min(rangeForSecondCar + 5, 100);
            rangeForThirdCar = Mathf.Min(rangeForThirdCar + 4, 60);
        
            yield return new WaitForSeconds(10); 
        }
    }

    private IEnumerator carSpeedCalculator()
    {
        while (true)
        {
            avarageCarSpeed = Mathf.Min(avarageCarSpeed + 2, 40);
            yield return new WaitForSeconds(15);
        }
    }

    private float getRandomSpeed()
    {   
        carSpeed = avarageCarSpeed;
        int randomIndex = Random.Range(0, 100);
        if (randomIndex >= 75)
        {
            carSpeed += 5;

        }else if (randomIndex <= 35)
        {
            carSpeed = carSpeed - 5;
        }
        
        return carSpeed;
        
    }
    
    private IEnumerator AnimateGold(GameObject goldObject)
    {
        float bounceSpeed = 3f; 
        float bounceHeight = 1f; 
        float rotationSpeed = 130f;

        Vector3 originalPosition = goldObject.transform.position;

        while (goldObject != null)
        {
            float newY = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
            goldObject.transform.position = originalPosition + new Vector3(0, newY, 0);

            // Dönme hareketi
            goldObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

}

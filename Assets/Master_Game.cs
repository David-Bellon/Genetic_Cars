using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class Master_Game : MonoBehaviour
{
    private bool carDead;
    private bool carScore;
    private car_moves c;
    private int maxCars;
    public int numberCar;
    public GameObject carToSpawn;
    public GameObject spawnLocation;
    private Text myText;
    private HttpClient client;
    private List<car_moves> carList = new List<car_moves>();
    // Start is called before the first frame update
    void Start()
    {
        c = gameObject.GetComponentInChildren<car_moves>();
        maxCars = 19;
        numberCar = 0;
    }

    // Update is called once per frame
    void Update()
    {
        c = gameObject.GetComponentInChildren<car_moves>();
        if (c.isDead)
        {
            if (numberCar < maxCars)
            {
                carList.Add(c);
                numberCar = numberCar + 1;
                SpawnObject();
                Destroy(c.gameObject);
            }
            else
            {
                if (numberCar == maxCars)
                {
                    carList.Add(c);
                    numberCar = numberCar + 1;
                }
                else
                {
                    if (numberCar == maxCars + 1)
                    {
                        numberCar = numberCar + 1;
                        carList.Sort((y, x) => x.score.CompareTo(y.score));
                        for (int i = 0; i < carList.Count; i++)
                        {
                            Debug.Log("Car Number:" + carList[i].car_number + " Score: " + carList[i].score + " Neural net: " + carList[i].filename);
                        }
                        UpdateValues(carList[0].filename, carList[1].filename, carList);
                        System.Threading.Thread.Sleep(1000);
                        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
        }
    }

    private void SpawnObject()
    {
        var newCar = Instantiate(carToSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
        newCar.transform.parent = transform;
        carToSpawn = newCar;
    }

    private async void UpdateValues(string value1, string value2, List<car_moves> carScores)
    {
        client = new HttpClient();
        var values = new Dictionary<string, string>
        {
        };
        for (int i = 0; i < carScores.Count; i++)
        {
            values[carScores[i].filename] = carScores[i].score.ToString();
        }
        var content = new FormUrlEncodedContent(values);
        var response = await client.PostAsync("http://127.0.0.1:5000/home", content);
        var responseString = await response.Content.ReadAsStringAsync();
        Debug.Log(responseString);
    }
}

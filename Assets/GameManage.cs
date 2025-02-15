using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public GameObject stockPrefab;
    public GameObject starPrefab;
    public float minValue;
    public float maxValue;
    public float timeToSpam;
    // Start is called before the first frame update
    void Start()
    {
        minValue = -10;
        maxValue = 10;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        timeToSpam = 2f;
        if (currentIndex >= 3)
        {
            timeToSpam = 1f;
        }
        InvokeRepeating("stockSpam", 1f, timeToSpam);
        InvokeRepeating("starSpam", 1f, 2f);
    }

    public void CancelInvoke()
    {
        CancelInvoke("stockSpam");
        CancelInvoke("starSpam");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stockSpam()
    {
        Vector3 v = new Vector3(Random.Range(minValue, maxValue), 6f);
        Instantiate(stockPrefab, v, Quaternion.identity);
    }

    public void starSpam()
    {
        Vector3 v = new Vector3(Random.Range(minValue, maxValue), 6f);
        Instantiate(starPrefab, v, Quaternion.identity);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

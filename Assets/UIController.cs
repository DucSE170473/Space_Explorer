using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI textScore;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InscreaseScore()
    {
        score = score + 1;
        UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateUI()
    {
        textScore.SetText("Score: " + score);
    }
}

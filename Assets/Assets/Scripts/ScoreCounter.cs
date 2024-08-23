using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{

    public static ScoreCounter Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool gameOver = false;

    [SerializeField] private TMP_Text target;
    private float score = 0;
    // Update is called once per frame
    void Update()
    {
        if(gameOver)
            return;
        score += 8 * Time.deltaTime;

        target.text = $"Score: {(int)score}";
    }
}

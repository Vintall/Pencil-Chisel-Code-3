using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text target;
    private float score = 0;
    // Update is called once per frame
    void Update()
    {
        score += 8 * Time.deltaTime;

        target.text = $"Score: {(int)score}";
    }
}

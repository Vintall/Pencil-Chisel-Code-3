using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcingSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer icingForeground;
    [SerializeField] private Transform icingCollider;

    private float icingValue = 50;
    [SerializeField] private float icingSpeed = 1;
    void Update()
    {
        icingValue += icingSpeed * Time.deltaTime;
        icingForeground.size = new Vector2(icingValue, 1);
        icingCollider.localPosition = new Vector3(icingValue, 0, 0);
    }
}

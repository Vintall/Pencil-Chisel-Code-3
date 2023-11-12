using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcingSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer icingForeground;
    [SerializeField] private Transform icingCollider;
    [SerializeField] private Transform character;

    private float icingValue = 50;
    [SerializeField] private float icingSpeed = 1;
    void Update()
    {
        icingValue += icingSpeed * Time.deltaTime;
        var dist = Vector3.Distance(icingCollider.transform.position, character.position);
        if (dist > 50f)
            icingValue += icingSpeed * Time.deltaTime * 3;
        
        icingForeground.size = new Vector2(icingValue, 1);
        icingCollider.localPosition = new Vector3(icingValue, 0, 0);
    }
}

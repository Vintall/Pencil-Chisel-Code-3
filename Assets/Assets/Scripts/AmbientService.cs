using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmbientService : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private List<AudioClip> melodies;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (!_source.isPlaying)
        {
            _source.clip = melodies[Random.Range(0, melodies.Count)];
            _source.Play();
        }
    }
}

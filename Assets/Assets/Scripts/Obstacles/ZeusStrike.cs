using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class ZeusStrike : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Collider2D _collider2D;
        
        public void Start()
        {
            DOTween.Sequence()
                .AppendCallback(_particleSystem.Play)
                .AppendInterval(0.8f)
                .AppendCallback(() =>
                {
                    _collider2D.enabled = true;
                    _audioSource.Play();
                });
            
        }
    }
}

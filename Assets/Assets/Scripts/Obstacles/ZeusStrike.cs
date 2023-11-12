using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class ZeusStrike : MonoBehaviour, IObstacle
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject _lethalCollider;
        [SerializeField] private GameObject _triggerCollider;
        
        public void Activate()
        {
            _triggerCollider.gameObject.SetActive(false);
            
            DOTween.Sequence()
                .AppendCallback(_particleSystem.Play)
                .AppendInterval(0.8f)
                .AppendCallback(() =>
                {
                    
                    _lethalCollider.gameObject.SetActive(true);
                    
                    _audioSource.Play();
                })
                .AppendInterval(0.4f)
                .AppendCallback(() =>
                {
                    _lethalCollider.gameObject.SetActive(false);

                })
                .AppendInterval(3f)
                .AppendCallback(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}

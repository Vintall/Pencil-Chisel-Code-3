using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Obstacles
{
    public class ZeusStrike : MonoBehaviour, IObstacle
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _warningSource;
        [SerializeField] private GameObject _lethalCollider;
        [SerializeField] private GameObject _triggerCollider;
        [SerializeField] private Transform _trap;

        private void Start()
        {
            _trap.localPosition = new Vector3(Random.Range(-4, 4), 0, 0);
        }

        public void Activate()
        {
            _triggerCollider.gameObject.SetActive(false);
            _warningSource.Play();
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

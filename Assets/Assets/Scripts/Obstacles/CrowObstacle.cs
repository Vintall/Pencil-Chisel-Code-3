using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Obstacles
{
    public class CrowObstacle : MonoBehaviour, IObstacle
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject _lethalCollider;
        [SerializeField] private GameObject _triggerCollider;
        [SerializeField] private GameObject crow;

        [SerializeField] private List<AudioClip> crowVoice;

        private void Start()
        {
            _particleSystem.Play();
            _audioSource.clip = crowVoice[Random.Range(0, crowVoice.Count)];
        }

        public void Activate()
        {
            var targetPosition = CharacterController.Instance.transform.position;
            var vector = (targetPosition - crow.transform.position) * 2;

            _triggerCollider.gameObject.SetActive(false);
            
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _audioSource.Play();
                })
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    targetPosition = CharacterController.Instance.transform.position;
                    vector = (targetPosition - crow.transform.position) * 2;
                })
                .Append(crow.transform.DOMove(crow.transform.position + vector, 3f)
                    .SetEase(Ease.OutCubic))
                .AppendCallback(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}

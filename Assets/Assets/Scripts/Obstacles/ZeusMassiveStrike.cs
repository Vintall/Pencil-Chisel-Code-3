using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using DG.Tweening;
using UnityEngine;

public class ZeusMassiveStrike : MonoBehaviour, IObstacle
{
    [SerializeField] private List<ParticleSystem> _particleSystem;
    [SerializeField] private List<AudioSource> _audioSource;
    [SerializeField] private List<GameObject> _lethalCollider;

    [SerializeField] private Collider2D _triggerCollider;

    public void Activate()
    {
        _triggerCollider.gameObject.SetActive(false);
        
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                for (int i = 0; i < _particleSystem.Count; ++i)
                    _particleSystem[i].Play();
            })
            .AppendInterval(2f)
            .AppendCallback(() =>
            {
                for (int i = 0; i < _particleSystem.Count; ++i)
                {
                    _lethalCollider[i].gameObject.SetActive(true);
                }
                _audioSource[0].Play();
                _audioSource[1].Play();
            })
            .AppendInterval(0.4f)
            .AppendCallback(() =>
            {
                for (int i = 0; i < _particleSystem.Count; ++i)
                _lethalCollider[i].gameObject.SetActive(false);

            })
            .AppendInterval(3f)
            .AppendCallback(() =>
            {
                Destroy(gameObject);
            });
    }
}

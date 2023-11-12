using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace Assets.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem sisyphusRunning;
        [SerializeField] private ParticleSystem sisyphusDeath;
        [SerializeField] private Transform characterTransform;
        [SerializeField, Range(1f, 10f)] private float speedUphill;
        [SerializeField, Range(1f, 10f)] private float speedDownhill;
        [SerializeField, Range(1f, 500f)] private float rotationSpeed;
        [SerializeField] private Transform boulderTransform;
        [SerializeField] private Sprite fallingAnimationSprite;

        private static CharacterController _instance;
        public static CharacterController Instance => _instance;
        private Sequence deathSequence = null;
        private void Awake()
        {
            _instance = this;
            
            deathSequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    boulderTransform.eulerAngles = Vector3.zero;
                    DOTween.Sequence()
                        .Append(boulderTransform.DORotate(Vector3.forward * 90f, 0.5f).SetEase(Ease.Linear))
                        .Append(boulderTransform.DORotate(Vector3.forward * 180f, 0.5f).SetEase(Ease.Linear))
                        .Append(boulderTransform.DORotate(Vector3.forward * 270f, 0.5f).SetEase(Ease.Linear))
                        .Append(boulderTransform.DORotate(Vector3.forward * 360f, 0.5f).SetEase(Ease.Linear))
                        .SetLoops(-1);
                })
                .Join(boulderTransform.DOLocalMove(Vector3.left * 100f, 20f)).Pause();
        }

        private void Start()
        {
            sisyphusRunning.time = 0.5f;
            sisyphusRunning.Play();
        }

        private bool gameOver = false;
        void Update()
        {
            if(gameOver)
                return;
            
            float xAxis = Input.GetAxis("Horizontal");

            if (xAxis != 0)
            {
                sisyphusRunning.Play();
                var speed = xAxis > 0 ? speedUphill : speedDownhill;
                var handledSpeed = Time.deltaTime * speed * xAxis;
                characterTransform.position += handledSpeed * characterTransform.right;
                boulderTransform.Rotate(Vector3.forward, handledSpeed * -rotationSpeed, Space.World);
            }
            else
            {
                sisyphusRunning.Pause();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("ObstacleTrigger"))
                ObstacleTrigger(other);
            
            if(other.gameObject.CompareTag("LethalCollider"))
                DeathTrigger(other);
        }

        private void ObstacleTrigger(Collider2D other)
        {
            other.transform.parent.GetComponent<IObstacle>().Activate();
            
        }

        private void DeathTrigger(Collider2D other)
        {
            sisyphusRunning.gameObject.SetActive(false);
            sisyphusDeath.gameObject.SetActive(true);
            deathSequence.Play();
            GameOverCanvas.Instance.OnGameOver();
            gameOver = true;
        }
    }
}

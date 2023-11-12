using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem playerAnimation;
        [SerializeField] private Transform characterTransform;
        [SerializeField, Range(1f, 10f)] private float speedUphill;
        [SerializeField, Range(1f, 10f)] private float speedDownhill;
        [SerializeField, Range(1f, 500f)] private float rotationSpeed;
        [SerializeField] private Transform boulderTransform;
        [SerializeField] private Sprite fallingAnimationSprite;

        private static CharacterController _instance;
        public static CharacterController Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            playerAnimation.time = 0.5f;
            playerAnimation.Play();
        }

        void Update()
        {
            float xAxis = Input.GetAxis("Horizontal");

            if (xAxis != 0)
            {
                playerAnimation.Play();
                var speed = xAxis > 0 ? speedUphill : speedDownhill;
                var handledSpeed = Time.deltaTime * speed * xAxis;
                characterTransform.position += handledSpeed * characterTransform.right;
                boulderTransform.Rotate(Vector3.forward, handledSpeed * -rotationSpeed, Space.World);
            }
            else
            {
                playerAnimation.Pause();
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
            
            Debug.LogError($"Game Over");
        }
    }
}

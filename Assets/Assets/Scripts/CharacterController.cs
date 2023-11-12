using DG.Tweening;
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

        void Update()
        {
            float xAxis = Input.GetAxis("Horizontal");

            if (xAxis != 0)
            {
                playerAnimation.Play();
                if (xAxis > 0)
                {
                    characterTransform.position += Time.deltaTime * speedUphill * characterTransform.right;
                    boulderTransform.Rotate(Vector3.forward, -rotationSpeed * speedUphill * Time.deltaTime, Space.World);
                }
                else
                {
                    characterTransform.position -= Time.deltaTime * speedDownhill * characterTransform.right;
                    boulderTransform.Rotate(Vector3.forward, rotationSpeed * speedDownhill * Time.deltaTime, Space.World);
                }
            }
            else
            {
                playerAnimation.Pause();
            }
        }
    }
}

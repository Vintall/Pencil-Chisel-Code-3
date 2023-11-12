using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class BindPoint : MonoBehaviour
    {
        [SerializeField] private Transform pointTransform;
        [SerializeField] private Color pointColor;
        public Transform Transform => pointTransform;

        private void OnDrawGizmos()
        {
            Gizmos.color = pointColor;
            Gizmos.DrawWireCube(pointTransform.position, Vector3.one);
        }
    }
}

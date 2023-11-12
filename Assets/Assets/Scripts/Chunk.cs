using UnityEngine;

namespace Assets.Scripts
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private BindPoint leftBindPoint;
        [SerializeField] private BindPoint rightBindPoint;

        public BindPoint LeftBindPoint => leftBindPoint;
        public BindPoint RightBindPoint => rightBindPoint;

        [HideInInspector] public Chunk leftChunk;
        [HideInInspector] public Chunk rightChunk;
    }
}

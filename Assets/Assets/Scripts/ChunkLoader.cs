using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ChunkLoader : MonoBehaviour
    {
        [SerializeField] private GameObject chunkPrefab;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Chunk initialChunk;

        private void Start()
        {
            Chunk chunkObject = Instantiate(chunkPrefab).GetComponent<Chunk>();


            var dist = initialChunk.RightBindPoint.Transform.position - initialChunk.transform.position;
            var newDistance = chunkObject.transform.position - chunkObject.LeftBindPoint.Transform.position;

            chunkObject.transform.position = initialChunk.transform.position + dist + newDistance;
        }

        void Update()
        {
            
        }
    }
}

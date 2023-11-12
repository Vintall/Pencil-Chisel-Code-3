using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class ChunkLoader : MonoBehaviour
    {
        static private ChunkLoader _instance;
        public static ChunkLoader Instance => _instance;
        
        [SerializeField] private GameObject chunkPrefab;
        [SerializeField] private List<GameObject> obstaclePrefabs;

        [SerializeField] private Camera playerCamera;
        [SerializeField] private List<Chunk> chunks;
        public List<Chunk> Chunks => chunks;

        private void Awake()
        {
            _instance = this;
            chunks = new List<Chunk>();
            var firstChunk = SpawnChunk();
            var chunkTransform = firstChunk.transform;
            chunkTransform.position = transform.position;

            SpawnChunk(firstChunk, false);
            SpawnChunk(firstChunk, true);
        }

        private void Update()
        {
            for (var i = 0; i < chunks.Count; ++i)
            {
                var iterChunk = chunks[i];
                var cameraPos = playerCamera.transform.position.x;
                var chunkRightPos = iterChunk.RightBindPoint.transform.position.x;
                var chunkLeftPos = iterChunk.LeftBindPoint.transform.position.x;
                
                if (cameraPos > chunkLeftPos && cameraPos < chunkRightPos)
                {
                    if (chunks[i].leftChunk == null)
                        SpawnChunk(iterChunk, false);

                    if (chunks[i].rightChunk == null)
                        SpawnChunk(iterChunk, true);
                    
                    RemoveUnusedChunks(iterChunk);
                    
                    return;
                }
            }
        }

        public void RemoveUnusedChunks(Chunk currentChunk)
        {
            List<Chunk> removable = new List<Chunk>();
            for (var i = 0; i < chunks.Count; i++)
            {
                var iterChunk = chunks[i];
                if (iterChunk != currentChunk
                    && (currentChunk.leftChunk == null || iterChunk != currentChunk.leftChunk)
                    && (currentChunk.rightChunk == null || iterChunk != currentChunk.rightChunk))
                    removable.Add(chunks[i]);
            }

            for (var i = removable.Count - 1; i >= 0; --i)
            {
                chunks.Remove(removable[i]);

                if (removable[i].leftChunk != null)
                {
                    removable[i].leftChunk.rightChunk = null;
                    removable[i].leftChunk = null;
                }
                if (removable[i].rightChunk != null)
                {
                    removable[i].rightChunk.leftChunk = null;
                    removable[i].rightChunk = null;
                }
                
                Destroy(removable[i].gameObject);
            }
        }

        public Chunk SpawnChunk()
        {
            var chunk = Instantiate(chunkPrefab).GetComponent<Chunk>();
            chunk.transform.parent = transform;
            chunks.Add(chunk);

            for (int i = 0; i < 7; ++i)
                SpawnObstacle(chunk);
            
            return chunk;
        }

        public GameObject SpawnObstacle(Chunk chunk)
        {
            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)]);
            obstacle.transform.parent = chunk.transform;
            obstacle.transform.localPosition = new Vector3(Random.Range(-40, 32), 2, 0);
            return obstacle;
        }
        
        public Chunk SpawnChunk(Chunk parentChunk, bool side) // false - left, true - right
        {
            var chunk = SpawnChunk();
            
            var dist = parentChunk.RightBindPoint.Transform.position - parentChunk.transform.position;
            var newDistance = chunk .transform.position - chunk .LeftBindPoint.Transform.position;

            if (!side)
            {
                chunk.transform.position = parentChunk.transform.position - dist - newDistance;
                chunk.rightChunk = parentChunk;
                parentChunk.leftChunk = chunk;
            }
            else
            {
                chunk.transform.position = parentChunk.transform.position + dist + newDistance;
                chunk.leftChunk = parentChunk;
                parentChunk.rightChunk = chunk;
            }
            
            return chunk;
        }
    }
}

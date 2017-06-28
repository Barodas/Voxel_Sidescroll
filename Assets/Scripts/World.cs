using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
    public GameObject chunkPrefab;

    void Start()
    {
        for (int x = -2; x < 2; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                CreateChunk(x * 16, y * 16);
            }
        }
    }

    public Chunk GetChunk(int x, int y)
    {
        WorldPos pos = new WorldPos();
        float multiple = Chunk.chunkSize;
        pos.x = Mathf.FloorToInt(x / multiple) * Chunk.chunkSize;
        pos.y = Mathf.FloorToInt(y / multiple) * Chunk.chunkSize;
        Chunk containerChunk = null;
        chunks.TryGetValue(pos, out containerChunk);

        return containerChunk;
    }
    public Block GetBlock(int x, int y, int z)
    {
        Chunk containerChunk = GetChunk(x, y);
        if (containerChunk != null)
        {
            Block block = containerChunk.GetBlock(x - containerChunk.pos.x, y - containerChunk.pos.y, z);

            return block;
        }
        else
        {
            return new BlockAir();
        }
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        Chunk chunk = GetChunk(x, y);

        if (chunk != null)
        {
            chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, z, block);
            chunk.update = true;
        }
    }

    public void CreateChunk(int x, int y)
    {
        //the coordinates of this chunk in the world
        WorldPos worldPos = new WorldPos(x, y);

        //Instantiate the chunk at the coordinates using the chunk prefab
        GameObject newChunkObject = Instantiate(chunkPrefab, new Vector3(worldPos.x, worldPos.y), Quaternion.Euler(Vector3.zero)) as GameObject;

        //Get the object's chunk component
        Chunk newChunk = newChunkObject.GetComponent<Chunk>();

        //Assign its values
        newChunk.pos = worldPos;
        newChunk.world = this;

        //Add it to the chunks dictionary with the position as the key
        chunks.Add(worldPos, newChunk);

        //Add the following:
        for (int xi = 0; xi < 16; xi++)
        {
            for (int yi = 0; yi < 16; yi++)
            {
                if (yi <= 7)
                {
                    SetBlock(x + xi, y + yi, 0, new BlockGrass());
                    SetBlock(x + xi, y + yi, 1, new BlockGrass());
                }
                else
                {
                    SetBlock(x + xi, y + yi, 0, new BlockAir());
                    SetBlock(x + xi, y + yi, 1, new BlockAir());
                }
            }
        }
    }

    public void DestroyChunk(int x, int y)
    {
        Chunk chunk = null;
        if (chunks.TryGetValue(new WorldPos(x, y), out chunk))
        {
            Object.Destroy(chunk.gameObject);
            chunks.Remove(new WorldPos(x, y));
        }
    }
}

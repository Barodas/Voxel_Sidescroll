using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Chunk : MonoBehaviour
{
    private MeshFilter _filter;
    private MeshCollider _col;

    public Block[,,] blocks = new Block[chunkSize, chunkSize, chunkDepth]; // X, Y, Foreground/Background
    public World world;
    public WorldPos pos;
    public static int chunkSize = 16; // X, Y
    public static int chunkDepth = 2; // Foreground/Background
    public bool update = true;

    private void Start()
    {
        _filter = gameObject.GetComponent<MeshFilter>();
        _col = gameObject.GetComponent<MeshCollider>();

        // Example Chunk Generation
        //_blocks = new Block[chunkSize, chunkSize, chunkDepth];
        //for (int x = 0; x < chunkSize; x++)
        //{
        //    for (int y = 0; y < chunkSize; y++)
        //    {
        //        for (int z = 0; z < chunkDepth; z++)
        //        {
        //            _blocks[x, y, z] = new BlockAir();
        //        }
        //    }
        //}
        //_blocks[3, 5, 0] = new Block();
        //_blocks[4, 5, 0] = new BlockGrass();
        //UpdateChunk();
        // End Example Chunk Generation
    }

    void Update()
    {
        if (update)
        {
            update = false;
            UpdateChunk();
        }
    }

    public Block GetBlock(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
            return blocks[x, y, z];
        return world.GetBlock(pos.x + x, pos.y + y, z);
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            blocks[x, y, z] = block;
        }
        else
        {
            world.SetBlock(pos.x + x, pos.y + y, z, block);
        }
    }

    public static bool InRange(int index)
    {
        if (index < 0 || index >= chunkSize)
            return false;

        return true;
    }

    private void UpdateChunk()
    {
        MeshData meshData = new MeshData();
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkDepth; z++)
                {
                    meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                }
            }
        }
        RenderMesh(meshData);
    }

    private void RenderMesh(MeshData meshData)
    {
        _filter.mesh.Clear();
        _filter.mesh.vertices = meshData.vertices.ToArray();
        _filter.mesh.triangles = meshData.triangles.ToArray();
        _filter.mesh.uv = meshData.uv.ToArray();
        _filter.mesh.RecalculateNormals();

        _col.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();

        _col.sharedMesh = mesh;
    }
}
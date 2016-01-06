using UnityEngine;
using System.Collections;

public class TerrainChunk : MonoBehaviour
{
    public int X { get; private set; }
    public int Z { get; private set; }

    private Terrain Terrain { get; set; }
    private TerrainChunkSettings Settings { get; set; }
    
    public TerrainChunk(TerrainChunkSettings terrainChunkSettings, int x, int z)
    {
        Settings = terrainChunkSettings;
        X = x;
        Z = z;
    }

    public void CreateTerrain()
    {
        //Het maken van een nieuw terrein met daarbijbehorende resolutie.
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = Settings.HeightmapResolution;
        terrainData.alphamapResolution = Settings.AlphamapResolution;

        //Het maken van het terrein hoogte / lengte.
        var heightmap = GetHeightMap();
        terrainData.SetHeights(0, 0, heightmap);
        terrainData.size = new Vector3(Settings.Length, Settings.Height, Settings.Length);

        var newTerrainGameObject = Terrain.CreateTerrainGameObject(terrainData);
        newTerrainGameObject.transform.position = new Vector3(X * Settings.Length, 0, Z * Settings.Length);
        Terrain = newTerrainGameObject.GetComponent<Terrain>();
        Terrain.Flush();
    }

    private float[,] GetHeightMap()
    {
        var heightmap = new float[Settings.HeightmapResolution, Settings.HeightmapResolution];

        for(var zRes = 0; zRes < Settings.HeightmapResolution; zRes++)
        {
            for(var xRes = 0; xRes < Settings.HeightmapResolution; xRes++)
            {
                var xCoordinate = X + (float)xRes / (Settings.HeightmapResolution - 1);
                var zxCoordinate = Z + (float)zRes / (Settings.HeightmapResolution - 1);

                NoiseMethod method = Noise.noiseMethods[(int)Settings.noiseType][Settings.dimensions - 1];
                heightmap[zRes, xRes] = method(new Vector3(xRes, 0, zRes), 1);
            }
        }

        return heightmap;
    }
}

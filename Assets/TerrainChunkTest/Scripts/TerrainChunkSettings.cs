using UnityEngine;
using System.Collections;

public class TerrainChunkSettings : MonoBehaviour {
    
    public int HeightmapResolution { get; private set; }
    public int AlphamapResolution { get; private set; }

    public int Length { get; private set; }
    public int Height { get; private set; }

    public NoiseMethodType noiseType;

    [Range(1,3)]
    public int dimensions;

    public TerrainChunkSettings(int heightmapResolution, int alphamapResolution, int length, int height, NoiseMethodType noiseType, int dimensions)
    {
        HeightmapResolution = heightmapResolution;
        AlphamapResolution = alphamapResolution;
        Length = length;
        Height = height;
        this.noiseType = noiseType;
        this.dimensions = dimensions;
    }
}

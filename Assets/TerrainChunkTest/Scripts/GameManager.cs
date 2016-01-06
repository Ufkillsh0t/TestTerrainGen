using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Test();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Test()
    {
        var settings = new TerrainChunkSettings(129, 129, 100, 20, (NoiseMethodType)1, 3);
        var terrain = new TerrainChunk(settings, 0, 0);
        terrain.CreateTerrain();
    }
}

using UnityEngine;
using System.Collections;

public class TextureCreator : MonoBehaviour {

    // Resolution of the generated texture in width and height. (default 256x256)
    public int resolution = 256;

    // Unity texture class.
    private Texture2D texture;


	// When the object awakes we will generate the texture.
	void Awake()
    {
        // Assigning the texture variable to a new Texture2D object with RGB color and mipmapping.
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        // Naming the texture for identification.
        texture.name = "Procedural Texture";
        // Grabbing the Meshrenderer of the Quad object so we can assing the new texture to the Quad object.
        GetComponent<MeshRenderer>().material.mainTexture = texture;
    }
}

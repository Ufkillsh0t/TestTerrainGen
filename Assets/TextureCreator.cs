using UnityEngine;
using System.Collections;

public class TextureCreator : MonoBehaviour {

    // Resolution of the generated texture in width and height. (default 256x256)
    public int resolution = 256;

    // Unity texture class.
    private Texture2D texture;


	// When the object awakes we will generate the texture. <-- Only works if the code recompiles
    // When the object gets enabled with OnEnable you can change the color while Unity is still running.
	void OnEnable()
    {
        // Assigning the texture variable to a new Texture2D object with RGB color and mipmapping.
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        // Naming the texture for identification.
        texture.name = "Procedural Texture";
        // Grabbing the Meshrenderer of the Quad object so we can assing the new texture to the Quad object.
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        // Fills the texture with the color Red.
        FillTexture();
    }

    /// <summary>
    /// Fills the textur....
    /// </summary>
    private void FillTexture()
    {
        for(int y = 0; y < resolution; y++) // For every y-axis till resolution
        {
            for(int x = 0; x < resolution; x++) // For every x-axis till resolution
            {
                texture.SetPixel(x, y, Color.red); // Fill that current position with the color red.
            }
        }
        texture.Apply(); // Applies the texture.
    }
}

using UnityEngine;
using System.Collections;

public class TextureCreator : MonoBehaviour {

    // Resolution of the generated texture in width and height. (default 256x256)
    [Range(2, 512)] //Set a minimum and maximum res for the texture.
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
        // Won't let unity repeat the texture multiple times which will make weird texture artifacts at lower texture resolutions.
        texture.wrapMode = TextureWrapMode.Clamp;
        // Unity's default texture filtering is bilinear that's why a low res texture will still look smooth. To create the real look we will use point filtering;
        texture.filterMode = FilterMode.Point;
        // Grabbing the Meshrenderer of the Quad object so we can assing the new texture to the Quad object.
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        // Fills the texture with the color Red.
        FillTexture();
    }

    /// <summary>
    /// Fills the texture....
    /// </summary>
    public void FillTexture()
    {
        if(texture.width != resolution) // If the width of the texture isn't the same as the resolution resize the texture to the resolution.
        {
            texture.Resize(resolution, resolution);
        }
        float stepSize = 1f / resolution; // Number used to generate a certain color between 0f-1f foreach pixel.
        for(int y = 0; y < resolution; y++) // For every y-axis till resolution
        {
            for(int x = 0; x < resolution; x++) // For every x-axis till resolution
            {
                texture.SetPixel(x, y, new Color(x * stepSize, y * stepSize, 0f)); //Displays a certain green/red color at a certain axis based on the stepSize.
            }
        }
        texture.Apply(); // Applies the texture.
    }

    /*
    /// <summary>
    /// Fills the texture....
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
    */
}

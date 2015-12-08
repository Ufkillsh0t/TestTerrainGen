using UnityEngine;
using System.Collections;

public class TextureCreator : MonoBehaviour {

    // Resolution of the generated texture in width and height. (default 256x256)
    [Range(2, 512)] //Set a minimum and maximum res for the texture.
    public int resolution = 256;

    [Range(1, 3)] //Set which dimension of noise you want to use.
    public int dimension = 3;

    //Noise frequency;
    public float frequency = 1f;

    public NoiseMethodType type;

    // Unity texture class.
    private Texture2D texture;


    private void Update()
    {
        if (transform.hasChanged) // IF the quad is moved it changes the textures.
        {
            transform.hasChanged = false;
            FillTexture();
        }
    }

	// When the object awakes we will generate the texture. <-- Only works if the code recompiles
    // When the object gets enabled with OnEnable you can change the color while Unity is still running.
	void OnEnable()
    {
        if (texture == null)
        {
            // Assigning the texture variable to a new Texture2D object with RGB color and mipmapping.
            texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
            // Naming the texture for identification.
            texture.name = "Procedural Texture";
            // Won't let unity repeat the texture multiple times which will make weird texture artifacts at lower texture resolutions.
            texture.wrapMode = TextureWrapMode.Clamp;
            // Unity's default texture filtering is bilinear that's why a low res texture will still look smooth. To create the real look we will use point filtering;
            texture.filterMode = FilterMode.Trilinear;
            // Anisotropic filtering makes the texture look beter at angles.
            texture.anisoLevel = 9;
            // Grabbing the Meshrenderer of the Quad object so we can assing the new texture to the Quad object.
            GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
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

        Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f, -0.5f)); //midden linkeronderkant
        Vector3 point10 = transform.TransformPoint(new Vector3(0.5f, -0.5f)); //midden rechteronderkant
        Vector3 point01 = transform.TransformPoint(new Vector3(-0.5f, 0.5f)); //midden linkerbovenkant
        Vector3 point11 = transform.TransformPoint(new Vector3(0.5f, 0.5f)); //midden rechterbovenkant

        NoiseMethod method = Noise.noiseMethods[(int)type][dimension - 1]; //Welke noise methode er gebruikt moet worden.
        float stepSize = 1f / resolution; // Number used to generate a certain color between 0f-1f foreach pixel.
        for (int y = 0; y < resolution; y++) // For every y-axis till resolution
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize); // interpolate between two points on the y-axis.
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize); // interpolate between two points on the y-axis.

            for (int x = 0; x < resolution; x++) // For every x-axis till resolution
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize); // interpolate between two points on the x-axis and y-axis.
                float sample = method(point, frequency); // Waarde die uit de Noise methodes komt.
                if(type != NoiseMethodType.Value)
                {
                    sample = sample * 0.5f + 0.5f;
                }
                texture.SetPixel(x, y, Color.white * sample); //Displays a certain color of white/grey/black depending on the noise method.
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

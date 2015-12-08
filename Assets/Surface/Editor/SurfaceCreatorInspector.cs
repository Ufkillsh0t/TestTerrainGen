﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SurfaceCreator))]
public class SurfaceCreatorInspector : Editor
{
    private SurfaceCreator creator;

    private void OnEnable()
    {
        creator = target as SurfaceCreator; //Get a instance of the TextureCreator.
        Undo.undoRedoPerformed += RefreshCreator; //Assign the TextureCreator to the Undo.undoRedoPerformed delegate.
    }

    private void OnDisable()
    {
        Undo.undoRedoPerformed -= RefreshCreator; //Unassign the TextureCreator from the Undo.undoRedoPerformed delegate.
    }

    /// <summary>
    /// Refreshes the texture of the TextureCreator.
    /// </summary>
    private void RefreshCreator()
    {
        if (Application.isPlaying) //When the application is running/playing.
        {
            creator.Refresh();
        }
    }


    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck()) //If a change happens in playmode...
        {
            RefreshCreator(); //Draw a new texture using the FillTexture method in TextureCreator.
        }
    }
}

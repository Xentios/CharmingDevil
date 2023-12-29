using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class FaceMaker : MonoBehaviour
{

    [SerializeField]
    SkinnedMeshRenderer skinnedMeshRenderer;

    [ContextMenu ("Save")]
    public void SaveFace()
    {
        var x = new FaceShaper();

        for (int i = 0; i < 7; i++)
        {
            x.blendShapes[i] = skinnedMeshRenderer.GetBlendShapeWeight(i);
        }

        string folderPath = "Assets/Scripts/Scriptable Objects/Faces";

        string[] files = Directory.GetFiles(folderPath);

        // Get the count of files
        int fileCount = files.Length;
        fileCount /= 2;

        AssetDatabase.CreateAsset(x, folderPath+ "/"+ (fileCount+1)+ ".asset");

        // Save any changes to the asset database
        AssetDatabase.SaveAssets();

        // Refresh the asset database to ensure changes are immediately visible
        AssetDatabase.Refresh();
    }
 

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 30), "Click Me Anytime"))
        {
            SaveFace();
        }
    }
    private void HandleButtonClick()
    {
        throw new NotImplementedException();
    }
}

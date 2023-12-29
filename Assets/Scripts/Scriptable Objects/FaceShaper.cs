using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Face { Spoon, Cup, Bowl, Piece }

[Serializable]
public class BlendShapes
{
    public string name;
    [Range(0, 7)]    
    public int index;
    [Range(0,100f)]
    public float value;
}

[CreateAssetMenu(fileName = "FaceSO", menuName = "FaceShaper/BlendValues", order = 1)]
public class FaceShaper : ScriptableObject
{

    public string name;
    [Range(0, 100f)]
    public float[] blendShapes = new float[7];
    
    // public Ingredient[] potionIngredients;

}

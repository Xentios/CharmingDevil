using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaceManager : MonoBehaviour
{
    [SerializeField]
    private List<SkinnedMeshRenderer> faces;

    [SerializeField]
    private List<Camera> cameras;


    [SerializeField]
    private List<FaceShaper> shapedFaces;

    private IEnumerator Start2()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            //foreach (var face in faces)
            //{
            //    face.SetBlendShapeWeight(Random.Range(0, 7), Random.Range(0, 100f));
            //}
            SetAllFaces(Random.Range(0, 7), Random.Range(0, 100f));
        }
    }


     [ContextMenu ("MakeAFace")]
    public void ShapeRandomSetFace()
    {
        SetAllFacesBasedOnSO(shapedFaces[Random.Range(0,shapedFaces.Count)]);
    }

     public void SetAllFacesBasedOnSO(FaceShaper faceToMake)

    {
        for (int i = 0; i < faceToMake.blendShapes.Length; i++)
        {
            SetAllFaces(i, faceToMake.blendShapes[i]);
        }

    }

    private void SetAllFaces(int index,float value)
    {
        if(index==1 ||index > 4)
        {
            foreach (var face in faces)
            {
                face.SetBlendShapeWeight(index, value);
            }
        }
        else
        {
            foreach (var face in faces)
            {
                if (face == faces[1]) continue;
                if (Random.value > 0.5) continue;
                face.SetBlendShapeWeight(index, value);
            }
        }
      
    }

}

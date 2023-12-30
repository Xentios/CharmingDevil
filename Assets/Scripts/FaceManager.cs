using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public enum FacePos {  Left,Middle,Right};

public class FaceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameWinCanvas;

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private DesireManager desireManager;

    public int StreamerPoints = 1000;

    [SerializeField]
    private TextMeshProUGUI scoreTextField;

    [SerializeField]
    private List<SkinnedMeshRenderer> visualFaces;
    [SerializeField]
    private List<TextMeshProUGUI> faceNames;

    [SerializeField]
    private List<CameraSpinner> cameras;


    [SerializeField]
    private List<FaceShaper> shapedFaces;

    [SerializeField]
    private List<bool> lockedFaces;


    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private AudioClip lose;
    [SerializeField]
    private AudioSource music;

    public UnityEvent onSpinEnd;
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
            foreach (var face in visualFaces)
            {
                face.SetBlendShapeWeight(index, value);
            }
        }
        else
        {
            foreach (var face in visualFaces)
            {
                if (face == visualFaces[1]) continue;
                if (Random.value > 0.5) continue;
                face.SetBlendShapeWeight(index, value);
            }
        }
      
    }


    public void SetFacesOnebyOne()
    {
        var multiplier = 1;
        var secondMultipler = 0;
        foreach (var lockedValue in lockedFaces)
        {
            multiplier *= lockedValue ? (2+ secondMultipler) : 1;
            secondMultipler += 1;
        }

        StreamerPoints -= 10* multiplier;
        if(StreamerPoints<0)
        {
            music.Stop();
            audioSource.Stop();
            audioSource.pitch = 0.5f;
            audioSource.PlayOneShot(lose);
            gameOverCanvas.SetActive(true);
        }
        scoreTextField.text = ""+StreamerPoints;
        StartCoroutine(SetFacesDelayed());
    }

    internal void Win()
    {
        music.Stop();
        audioSource.Stop();      
        gameWinCanvas.SetActive(true);
    }

    IEnumerator SetFacesDelayed()
    {
        for (int i = 0; i < 3; i++)
        {
            if (lockedFaces[i] == true) continue;
            SetFace((FacePos)i, shapedFaces[Random.Range(0, shapedFaces.Count)]);
            yield return new WaitForSeconds(0.3f);
        }

        desireManager.UpdateDesiresNegative(faceNames[0].text, faceNames[1].text,faceNames[2].text);
    }



    private void SetFace(FacePos facePos, FaceShaper faceToMake)
    {
        SpinCamera(facePos);
        EditText(facePos, faceToMake.name);
        for (int i = 0; i < faceToMake.blendShapes.Length; i++)
        {
            visualFaces[(int) facePos].SetBlendShapeWeight(i, faceToMake.blendShapes[i]);
        }

    }

    private void EditText(FacePos facePos, string name)
    {
        faceNames[(int) facePos].text = name;
    }

    private void SpinCamera(FacePos facePos)
    {
        //cameras[(int) facePos].onSpinEnd.AddListener()
        audioSource.PlayOneShot(audioClip);
        cameras[(int) facePos].SpinCamera();
    }


    public void LockFace(int index)
    {
        lockedFaces[index] = !lockedFaces[index];
    }

}

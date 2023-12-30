using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesireManager : MonoBehaviour
{
    [SerializeField]
    FaceManager faceManager;

    [SerializeField]
    private GameObject panelRoot;

    [SerializeField]
    private GameObject desirePrefab;

    [SerializeField]
    private List<FaceShaper> shapedFaces;


    private int[] desirePersentange;
    private GameObject[] desiresInstanced;

    private void Awake()
    {
        desirePersentange = new int[shapedFaces.Count];
        desiresInstanced= new GameObject[shapedFaces.Count];
    }

    public void UpdateDesiresPositive()
    {
        var rIndex = Random.Range(0, shapedFaces.Count);
        var name = shapedFaces[rIndex].name;
        var persentange= Random.Range(5, 25);     

        GameObject result;
        if (desirePersentange[rIndex] == 0)
        {
            result=Instantiate(desirePrefab, panelRoot.transform);
          
          
        }
        else
        {
            result = desiresInstanced[rIndex];         

        }
        desirePersentange[rIndex] = Mathf.Min(desirePersentange[rIndex] + persentange, 100);
        var hooker = result.GetComponent<EasyHooker>();
        hooker.imageToShowPercent.fillAmount = desirePersentange[rIndex] / 100f;
        hooker.percentTextfield.text = " " + desirePersentange[rIndex] + "%";
        hooker.nameTextfield.text = name;
        desiresInstanced[rIndex] = result;

        SortDesires();

    }

    private void UpdateVisual(int index)
    {
        var result = desiresInstanced[index];
        var hooker = result.GetComponent<EasyHooker>();
        hooker.imageToShowPercent.fillAmount = desirePersentange[index] / 100f;
        hooker.percentTextfield.text = " " + desirePersentange[index] + "%";


        if (desirePersentange[index] == 0)
        {
            desiresInstanced[index] = null;
            GameObject.Destroy(hooker.gameObject);
        }
    }

    public void UpdateDesiresNegative(string face1, string face2, string face3)
    {
        var index = 0;
        var result = 0;
       if(face1==face2 && face1 == face3)
        {
            index=GetIndexOfDesire(face1);
            result = 1000;
        }
        else if(face1==face2 || face1==face3){
            index = GetIndexOfDesire(face1);
            result = 300;
        }
        else if(face2==face3){
            index = GetIndexOfDesire(face2);
            result = 300;
        }
        else
        {
            return;
        }

        if (desiresInstanced[index] == null) return;

        var hooker=desiresInstanced[index].GetComponent<EasyHooker>();
        result=(int) (result*hooker.imageToShowPercent.fillAmount);

        ReduceADesire(index, (int) (result/10));

        faceManager.StreamerPoints += result;
    }


    private void ReduceADesire(int index,int result)
    {
        if (result < 0) return;
        if (desiresInstanced[index] == null) return;

        desirePersentange[index] = Mathf.Max(0, desirePersentange[index] - result);

        UpdateVisual(index);

       // var hooker = desiresInstanced[index].GetComponent<EasyHooker>();
       //var reduction = hooker.imageToShowPercent.fillAmount - (result / 800f);

        //hooker.imageToShowPercent.fillAmount = Mathf.Max(0, reduction);
        //hooker.imageToShowPercent.fillAmount = desirePersentange[rIndex] / 100f;
       
    }

    private int GetIndexOfDesire(string name)
    {
        for (int i = 0; i < shapedFaces.Count; i++)
        {
           if(shapedFaces[i].name == name)
            {
                return i;
            }
        }

        return  -1;
    }

    public void SortDesires()
    {
        List<EasyHooker> rectTransforms = new();
        for (int i = 0; i < desiresInstanced.Length; i++)
        {
            if (desirePersentange[i] != 0)
            {
                rectTransforms.Add(desiresInstanced[i].GetComponent<EasyHooker>());
            }
        }

        rectTransforms.Sort((a, b) => b.imageToShowPercent.fillAmount.CompareTo(a.imageToShowPercent.fillAmount));

        var x = 0;
        foreach (var item in rectTransforms)
        {
            item.transform.SetSiblingIndex(x);
            x++;
        }

    }
}

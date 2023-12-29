using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CameraSpinner : MonoBehaviour
{

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private AnimationCurve spinEase;

    public UnityEvent onSpinEnd;

 
    void Start()
    {
        //SpinCamera();
       
        StartCoroutine(RenderCamera());
        
    }

    IEnumerator RenderCamera()
    {
        while (true)
        {            
            camera.Render();
            yield return new WaitForSeconds(0.03f);           
        }
    }
   
      

    [ContextMenu ("SpinCam")]
    public void SpinCamera()
    {      
        transform.DOBlendableRotateBy(Vector3.right * 360, 1f, RotateMode.WorldAxisAdd).SetEase(spinEase).OnComplete(StartDelay);                
    }

    private void StartDelay()
    {
        StartCoroutine(DelayedResponse());
    }

    IEnumerator DelayedResponse()
    {
        yield return new WaitForSeconds(0.5f);
        onSpinEnd?.Invoke();
        yield return new WaitForSeconds(0.5f);
        
    }
}

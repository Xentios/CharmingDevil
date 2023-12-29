using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraSpinner : MonoBehaviour
{

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private AnimationCurve spinEase;
    // Start is called before the first frame update
    void Start()
    {
        SpinCamera();
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
        transform.DOBlendableRotateBy(Vector3.right * 360, 1f, RotateMode.WorldAxisAdd).SetEase(spinEase);
    }
}

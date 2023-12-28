using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraSpinner : MonoBehaviour
{

    [SerializeField]
    private AnimationCurve spinEase;
    // Start is called before the first frame update
    void Start()
    {
        SpinCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu ("SpinCam")]
    public void SpinCamera()
    {
        transform.DOBlendableRotateBy(Vector3.right * 360, 1f, RotateMode.WorldAxisAdd).SetEase(spinEase);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodButton : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnClicked;

    MeshRenderer meshRenderer;
    [SerializeField]
    Material NotClickedMat, OnClickedMat;

    public bool CanUseButton;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();    
        meshRenderer.material = NotClickedMat;
        CanUseButton = true;
    }

    public void UseButton()
    {
        if (CanUseButton)
        {
            meshRenderer.material = OnClickedMat;
            CanUseButton = false;
            OnClicked?.Invoke();
        }
    }
    public void ResetButton()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, .5f, Random.Range(-3.0f,3.0f));
        CanUseButton = true;

        meshRenderer.material = NotClickedMat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnEaten;

    public bool CanEat;

    void Awake()
    {
        CanEat = true;
    }

    public void EatFood()
    {
        if (CanEat)
        {
            Debug.Log("Got eaten");
            OnEaten?.Invoke();
            CanEat = false;
        }
    }
    public void ResetFood()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, .5f, Random.Range(-3.5f, 3.5f));
        CanEat = true;
    }

    private void OnEnable()
    {
        ResetFood();
    }
}

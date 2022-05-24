using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Camera.main.GetComponent<PipeManager>().RemovePipe(gameObject.transform.parent.gameObject);
        Destroy(gameObject.transform.parent.gameObject);
    }
}

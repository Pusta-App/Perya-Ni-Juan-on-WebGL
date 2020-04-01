
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class BC_Void_Calls : UnityEvent { }

public class BC_UTILS_VoidDrag : MonoBehaviour
{
    public float delayAwake;
    public BC_Void_Calls onAwake;
    public float delayStart;
    public BC_Void_Calls onStart;
    public float delayEnable;
    public BC_Void_Calls onEnable;
    public float delayDisable;
    public BC_Void_Calls onDisable;

    void Awake()
    {
        //if(this.gameObject.activeInHierarchy)
        //{
            StartCoroutine(Awaking());
        //}
    }

    IEnumerator Awaking()
    {
        yield return new WaitForSeconds(delayAwake);
        onAwake.Invoke();
    }

    void Start()
    {
        //if(this.gameObject.activeInHierarchy)
        //{
            StartCoroutine(Starting());
        //}
    }

    IEnumerator Starting()
    {
        yield return new WaitForSeconds(delayStart);
        onStart.Invoke();
    }

    void OnEnable()
    {
        //if(this.gameObject.activeInHierarchy)
        //{
            StartCoroutine(Enabling());
        //}
    }

    IEnumerator Enabling()
    {
        yield return new WaitForSeconds(delayEnable);
        onEnable.Invoke();
    }

    void OnDisable()
    {
        if(gameObject.activeInHierarchy)
        {
            StartCoroutine(Disaling());
        }
    }

    IEnumerator Disaling()
    {
        yield return new WaitForSeconds(delayDisable);
        onDisable.Invoke();
    }
}

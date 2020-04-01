
using UnityEngine;

public enum BC_UTILS_RotateAxis {
    Xaxis,
    Yaxis,
    Zaxis
}

public class BC_UTILS_3DRotate : MonoBehaviour
{
    public float speed = 10f;
    public bool inverse = false;
    public BC_UTILS_RotateAxis rotateAxis = BC_UTILS_RotateAxis.Yaxis;
    
    public Transform pivot;
    public Transform target;

    private Vector3 direction {
        get {
            if(rotateAxis == BC_UTILS_RotateAxis.Xaxis)
            {
                return pivot.right;
            }

            else if(rotateAxis == BC_UTILS_RotateAxis.Yaxis)
            {
                return pivot.up;
            }
            
            else
            {
                return pivot.forward;
            }
        }
    } 

    private float acceleration {
        get {
            if(inverse)
            {
                return speed * Time.deltaTime;
            }

            else
            {
                return -speed * Time.deltaTime;
            }
        }
    }

    void OnValidate()
    {
        if( pivot == null)
        {
            pivot = transform;
        }

        if( target == null)
        {
            target = transform;
        }
    }

    void Update()
    {
        target.RotateAround(pivot.position, direction, acceleration);
    }
}

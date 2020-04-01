
using UnityEngine;

public class PNJ_MR_Rotator : MonoBehaviour
{
    public float rotation;

    public float curSpeed = 0f; 
    
    private float decimator = 0f; 
    public bool dontCheckResult = true;
    public void Play()
    {
        curSpeed = Random.Range(750f, 1200f);
        dontCheckResult = false;
    }

    void Update()
    {    
        if( curSpeed > 0f )
        {
            transform.RotateAround(transform.position, transform.up, curSpeed * Time.deltaTime);
            decimator = 100f * Time.deltaTime;
            curSpeed -= decimator;

            rotation = transform.localRotation.eulerAngles.y;
        }

        else 
        {
            curSpeed = 0f;

            if( dontCheckResult )
                return;
            
            if( rotation >= 0f && rotation <= 30f )
            {
                CustomReference.Access.sabongWheel.CheckNow("Black", 5);
            }

            else if( rotation > 30f && rotation <= 60f )
            {
                CustomReference.Access.sabongWheel.CheckNow("Yellow", 4);
            }

            else if( rotation > 60f && rotation <= 90f )
            {
                CustomReference.Access.sabongWheel.CheckNow("White", 3);            
            }

            else if( rotation > 90f && rotation <= 120f )
            { 
                CustomReference.Access.sabongWheel.CheckNow("Blue", 2);           
            }

            else if( rotation > 120f && rotation <= 150f )
            {
                CustomReference.Access.sabongWheel.CheckNow("Green", 1);            
            }

            else if( rotation > 150f && rotation <= 180f )
            {  
                CustomReference.Access.sabongWheel.CheckNow("Red", 0);          
            }

            else if( rotation > 180f && rotation <= 210f )
            {
                CustomReference.Access.sabongWheel.CheckNow("Black", 5);            
            }

            else if( rotation > 210f && rotation <= 240f )
            {
                CustomReference.Access.sabongWheel.CheckNow("Yellow", 4);
            }

            else if( rotation > 240f && rotation <= 270f )
            {
                CustomReference.Access.sabongWheel.CheckNow("White", 3);            
            }

            else if( rotation > 270f && rotation <= 300f )
            { 
                CustomReference.Access.sabongWheel.CheckNow("Blue", 2);           
            }

            else if( rotation > 300f && rotation <= 330f )
            {
                CustomReference.Access.sabongWheel.CheckNow("Green", 1);            
            }

            else if( rotation > 330f && rotation <= 360f )
            {  
                CustomReference.Access.sabongWheel.CheckNow("Red", 0);          
            }

            dontCheckResult = true;
        }
    }
}
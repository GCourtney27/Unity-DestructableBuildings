using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public float scrollX = 0.0f;
    public float scrollY = 0.0f;
    public WheelCollider wheel = null;
    void Update()
    {
        // W: 1.2
        if (wheel)
            //scrollY = -wheel.rpm / 20.0f;
            //scrollY = -wheel.rpm / 500.0f;
            
        

        if (this.tag == "EnemyTrackLeft")
        {

            scrollY = 1.2f;
        }
        if (this.tag == "EnemyTrackRight")
        {

            scrollY = 1.2f;
        }

        float OffSetX = Time.time * scrollX;
        float OffSetY = Time.time * scrollY;
        GetComponent<Renderer>().material.SetTextureOffset("_BaseColorMap", new Vector2(OffSetX, OffSetY));
    }


    
}

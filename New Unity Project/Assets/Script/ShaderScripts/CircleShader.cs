using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShader : ShaderScripts
{
    public override Color calcColor(RaycastHit hitInfo)
    {
        Color fragColor;
        Vector2 circlePosition = new Vector2(.5f,.5f);
        float radius = .5f;
        
        if((hitInfo.textureCoord - circlePosition).magnitude < radius)
        {
            fragColor = Color.red;
        }
        else
        {
            fragColor = hitInfo.transform.GetComponent<Renderer>().sharedMaterial.color;
        }

        return fragColor;
    }
}

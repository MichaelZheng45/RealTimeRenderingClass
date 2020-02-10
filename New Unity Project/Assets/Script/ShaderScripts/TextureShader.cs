using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureShader : ShaderScripts
{
    public override Color calcColor(RaycastHit hitInfo)
    {
        Material currentMat = hitInfo.transform.GetComponent<Renderer>().material;

        Color fragColor;
        if (currentMat.mainTexture == true)
        {
            fragColor = (currentMat.mainTexture as Texture2D).GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
        }
        else
        {
            fragColor = Color.black;
        }

        return fragColor;
    }
}

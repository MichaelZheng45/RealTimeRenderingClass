
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambertDiffuseShader : ShaderScripts
{
    public Texture2D mainTexture;
    public Light mainLight;

    public override Color calcColor(RaycastHit hitInfo)
    {
        Color fragColor;

        if (mainLight && mainTexture)
        {

            Vector3 rayDir = mainLight.transform.position - hitInfo.point;

            Vector3 surfaceNorm = hitInfo.normal;

            float intensity = Vector3.Dot(surfaceNorm, rayDir.normalized);

            intensity = Mathf.Max(intensity, 0);

            Ray hit = new Ray(hitInfo.point, rayDir);
            float shadow = 1.0f;
            if(Physics.Raycast(hit,rayDir.magnitude))
            {
                shadow = .3f;
            }

            fragColor = mainTexture.GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y) * intensity * shadow;
        }
        else
        {
            return Color.black;
        }

        return new Color(fragColor.r, fragColor.g, fragColor.b, 1.0f);
    }
}

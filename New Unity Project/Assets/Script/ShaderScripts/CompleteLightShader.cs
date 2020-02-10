using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLightShader : ShaderScripts
{
    public Texture2D mainTexture;
    public Light mainLight;

    public override Color calcColor(RaycastHit hitInfo)
    {
        Color fragColor = Color.black;
        float insideRange = 1.0f;
        float insideShadow = 0.0f;
        Vector3 directionToLight =Vector3.zero;

        if (mainLight && mainTexture)
        {
            Vector3 lightDir = Vector3.zero;
            Vector3 position = hitInfo.point;
            Vector3 surfaceNorm = hitInfo.normal.normalized;
            switch (mainLight.type)
            {
                case LightType.Directional:
                    lightDir = -mainLight.transform.forward;
                    break;
                case LightType.Point:
                    Vector3 lightPos = mainLight.transform.position;
                    lightDir = (lightPos - position);

                    if (lightDir.magnitude <= mainLight.range)
                    {
                        //insideRange = 1.0f; 
                        insideRange = (lightDir.magnitude * lightDir.magnitude) / (mainLight.range * mainLight.range);
                        insideRange = 1.0f - insideRange;
                    }
                    directionToLight = lightDir.normalized;
                    break;
            }

            insideShadow = IsShadowed(hitInfo.point, directionToLight);

            float diff = Mathf.Max(Vector3.Dot(surfaceNorm, lightDir.normalized), 0, 0);
            float ambient = .1f;

            float specular;
            float specularStrength = 0.5f;

            Vector3 viewDir = (Camera.main.transform.position - position).normalized;
            Vector3 reflectDir = Vector3.Reflect(-lightDir.normalized, surfaceNorm);

            specular = Mathf.Pow(Mathf.Max(Vector3.Dot(viewDir, reflectDir), 0.0f), 32) * specularStrength;

            Vector3 rayDir = mainLight.transform.position - hitInfo.point;
            Ray hit = new Ray(hitInfo.point, rayDir);
            float shadow = 1.0f;
            if (Physics.Raycast(hit, rayDir.magnitude))
            {
                shadow = .3f;
            }
   
            fragColor = mainTexture.GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y) * mainLight.intensity * insideRange * (specular + ambient + diff) * (1.0f - insideShadow); //Make sure we dont go negative
            fragColor.a = 1.0f;
            return fragColor;
        }


        return Color.black;
    }

    float IsShadowed(Vector3 point, Vector3 directionToLight)
{
    if (Physics.Raycast(point, directionToLight))
    {
        return 1.0f;
    }
    else
        return 0.0f;
}
}




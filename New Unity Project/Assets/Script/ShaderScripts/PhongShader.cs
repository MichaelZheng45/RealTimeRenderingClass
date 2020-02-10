using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhongShader : ShaderScripts
{

    public Texture2D mainTexture;
    public Light mainLight;
    public override Color calcColor(RaycastHit hitInfo)
    {
        Color fragColor;

        if (mainLight && mainTexture)
        {

            Vector3 position = hitInfo.point;
            Vector3 surfaceNorm = hitInfo.normal.normalized;
            Vector3 lightPos = mainLight.transform.position;

            Vector3 lightDir = (lightPos - position).normalized;

            float diff = Mathf.Max(Vector3.Dot(surfaceNorm, lightDir), 0, 0);
            float ambient = .1f;

            float specular;
            float specularStrength = 0.5f;

            Vector3 viewDir = (Camera.main.transform.position - position).normalized;
            Vector3 reflectDir = Vector3.Reflect(-lightDir, surfaceNorm);

            specular = Mathf.Pow(Mathf.Max(Vector3.Dot(viewDir, reflectDir), 0.0f), 32) * specularStrength;

            Vector3 rayDir = mainLight.transform.position - hitInfo.point;
            Ray hit = new Ray(hitInfo.point, rayDir);
            float shadow = 1.0f;
            if (Physics.Raycast(hit, rayDir.magnitude))
            {
                shadow = .3f;
            }

            fragColor = mainTexture.GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y) * (ambient + diff + specular) * shadow;
            fragColor.a = 1.0f;
            return fragColor ;
        }

        return Color.black;
    }
}

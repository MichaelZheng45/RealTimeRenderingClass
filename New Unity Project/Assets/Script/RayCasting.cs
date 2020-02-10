using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public Texture2D canvsTexture;
    public float width;
    public float height;

    private void Awake()
    {
        canvsTexture = new Texture2D((int)width,(int)height);
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Ray hit = Camera.main.ViewportPointToRay(new Vector3((float)i / width, (float)j / height, Camera.main.nearClipPlane));
                RaycastHit rayHitCheck;
                
                if(Physics.Raycast(hit,out rayHitCheck))
                {
                    ShaderScripts shaderS = rayHitCheck.collider.gameObject.GetComponent<ShaderScripts>();
                    if(shaderS != null)
                    {
                        canvsTexture.SetPixel(i, j, shaderS.calcColor(rayHitCheck));
                    }
                    else
                    {
                        canvsTexture.SetPixel(i, j, Color.black);
                    }
                
                }
                else
                {
                    canvsTexture.SetPixel(i, j, Camera.main.backgroundColor);
                }
            }
        }

        canvsTexture.Apply();
    }

    // Update is called once per frame
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), canvsTexture);
    }
}

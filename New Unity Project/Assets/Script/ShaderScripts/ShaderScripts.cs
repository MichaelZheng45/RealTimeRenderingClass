using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderScripts : MonoBehaviour
{
    public virtual Color calcColor(RaycastHit hitInfo) { return Color.black; }
}

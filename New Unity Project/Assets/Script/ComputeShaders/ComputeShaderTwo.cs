using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderTwo : MonoBehaviour
{
    struct VecMatPair
    {
        public Vector3 point;
        public Matrix4x4 matrix;
    }

	public ComputeShader shader;

	void RunShader()
	{
		VecMatPair[] data = new VecMatPair[5];
		VecMatPair[] output = new VecMatPair[5];

		for(int i = 0; i < 5; i++)
		{
			data[i].point = new Vector3();
			data[i].matrix = new Matrix4x4();
		}

		ComputeBuffer buffer = new ComputeBuffer(data.Length, 76);
		buffer.SetData(data);
		int kernel = shader.FindKernel("Multiply");
		shader.SetBuffer(kernel, "dataBuffer", buffer);
		shader.Dispatch(kernel, data.Length, 1, 1);

		//
		buffer.GetData(output);
	}

	private void Awake()
	{
		RunShader();
	}
}

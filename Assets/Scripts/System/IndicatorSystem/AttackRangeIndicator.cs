using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class AttackRangeIndicator : ViewController
	{
		public Mesh mesh;
		public Material meshMaterial;
		public Material lineMaterial;
		
		private void Start()
		{
			mesh = new Mesh();

			meshMaterial = new Material(Shader.Find("Custom/UnlitTransparent"));
			meshMaterial.color = new Color(0f, 0f, 1.0f, 0.2f);
			meshMaterial.renderQueue = 4000;

			meshFilter.mesh = mesh;
			meshRender.material = meshMaterial;
			
			lineMaterial = new Material(Shader.Find("Custom/UnlitTransparent"));
			lineMaterial.color = new Color(0f, 0f, 1.0f, 0.4f);
			
			lineRender.material = lineMaterial;
		}
	}
}

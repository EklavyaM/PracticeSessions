Shader "ShaderTutorial/Tutorial01"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white"{}
		_NormalMap("Normal Map", 2D) = "bump"{}

		_Color("Color", Color) = (1, 1, 1, 1)

		_SomeValue("Some Value", Int) = 1
		_SomeRange("Some Range", Range(0.0, 1.0)) = 0.5
	}
	SubShader
	{
		Pass
		{

		}
	}
}
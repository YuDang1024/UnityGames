Shader "Custom/VortexShader"
{	
	// Shader的输入数据绑定到我们的材质
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		radius("Radius", Float) = 0.0
		angle("Angle", Float) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			float radius; // 扭曲范围的半径
			float angle; // 扭曲的角度;

			// 顶点
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				// uv变换
				float2 uv = v.uv; // 获取顶点的纹理坐标; [0, 1]

				// 这两个参数不能写死在Shader,因为我们要改变这个值
				// Shader才能够实时的

				uv -= float2(0.5, 0.5); // uv坐标就是以中心为原，左小角的[-0.5, -0.5],[0.5, 0.5]  

				float dist = length(uv); // 计算当前的坐标的长度，当前的坐标到纹理中心的距离
				float percent = (radius - dist) / radius;
				
				if ( percent < 1.0 && percent >= 0.0)  { // 半径范围之类
					// 扭曲算法
					float theta = percent * percent * angle * 8.0;
					float s = sin(theta);
					float c = cos(theta);
					uv = float2(dot(uv, float2(c, -s)), dot(uv, float2(s, c)));
					// end
				}

				uv += float2(0.5, 0.5); // 变换回我们的纹理坐标寻址的远点
				o.uv = uv; // 这样顶点，就有了对应纹理的坐标
				return o;
			}
			
			// 着色
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}

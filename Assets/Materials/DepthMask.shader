﻿Shader "Masked/DepthMask" {
	
	SubShader {
		// Render the mask after regular geometry, but before masked geometry and
		// transparent things.
		
		Tags {"Queue" = "Geometry+501" }
		//Tags { "ForceNoShadowCasting" = "True"}
		
		// Don't draw in the RGBA channels; just the depth buffer

		Lighting Off
		ZTest LEqual

		ColorMask 0
		ZWrite On
		
		// Do nothing specific in the pass:
		
		Pass {}
	}
}
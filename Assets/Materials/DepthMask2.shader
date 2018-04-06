Shader "Masked/DepthMask2" {
	
	SubShader {
		// Render the mask after regular geometry, but before masked geometry and
		// transparent things.
		
		Tags {"Queue" = "Geometry+503" }
		
		// Don't draw in the RGBA channels; just the depth buffer

		//Lighting Off
		//ZTest LEqual

		ColorMask 0
		ZWrite On
		
		// Do nothing specific in the pass:
		
		Pass {}
	}
}
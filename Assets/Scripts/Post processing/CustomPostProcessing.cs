using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Rendering; 
using UnityEngine.Rendering.Universal;





public class CustomPostProcessing : ScriptableRendererFeature 
{
    // parameters 
    public RenderPassEvent insertAtStage = RenderPassEvent.AfterRendering; 
    public Material material; 
    // data 
    CustomBlitPass blitPass; 





    //  Init  ------------------------------------------------------- 
    public override void Create ()
	{
		blitPass = new CustomBlitPass(insertAtStage, material); 
	}





    //  Rendering  -------------------------------------------------- 
	public override void AddRenderPasses (ScriptableRenderer renderer, ref RenderingData renderingData)
	{
        blitPass.Setup(renderer.cameraColorTarget); 
        renderer.EnqueuePass(blitPass); 
	}

}

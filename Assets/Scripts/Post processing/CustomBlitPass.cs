using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;





public class CustomBlitPass : ScriptableRenderPass
{
    // data 
    Material material; 
    RenderTargetIdentifier cameraColorTarget; 
    // camera texture descriptor 
    RenderTextureDescriptor cameraTextureDescriptor; 



    public CustomBlitPass (RenderPassEvent renderPassEvent, Material material) 
    {
        this.renderPassEvent = renderPassEvent; 
        this.material = material; 
    }





    //  Rendering setup  -------------------------------------------- 
    public void Setup (RenderTargetIdentifier cameraColorTarget) 
    {
        this.cameraColorTarget = cameraColorTarget; 
    }

    public override void Configure (CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor) 
    {
        this.cameraTextureDescriptor = cameraTextureDescriptor; 
    }





    //  Rendering  -------------------------------------------------- 
	public override void Execute (ScriptableRenderContext context, ref RenderingData renderingData) 
    {
        CommandBuffer cmd = CommandBufferPool.Get(); 

        int tempRT = 0; 
		cmd.GetTemporaryRT(tempRT, cameraTextureDescriptor); 

        Blit(cmd, cameraColorTarget, tempRT, material); 
        Blit(cmd, tempRT, cameraColorTarget); 
        // cmd.Blit(cameraColorTarget, tempRT, material); 
        // cmd.Blit(tempRT, cameraColorTarget); 

		cmd.ReleaseTemporaryRT(tempRT); 

        context.ExecuteCommandBuffer(cmd); 
        CommandBufferPool.Release(cmd); 

        // context.Submit(); 
	}
}

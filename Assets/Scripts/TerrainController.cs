using UnityEngine;

public class TerrainController
{
    public void HandleIceEffect(PlayerController player)
    {
        Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
        PhysicsMaterial2D catMaterialPhysics = player.catMaterialPhysics;
        player.isOnIce = true;
        playerBody.drag = 0f;
        playerBody.sharedMaterial = catMaterialPhysics;
    }

    public void HandleLavaEffect(PlayerController player)
    {
        Health healthScript = player.GetComponent<Health>();
        GameObject fireParticleSystem = player.fireParticleSystem;
        if (healthScript != null)
        {
            healthScript.TakeDamage(1);
        }
        fireParticleSystem.SetActive(true);
    }

    public void HandleExitIceEffect(PlayerController player)
    {
        Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
        player.isOnIce = false;
        playerBody.drag = 4f;
        playerBody.sharedMaterial = null;
    }

    public void HandleExitLavaEffect(PlayerController player)
    {
        GameObject fireParticleSystem = player.fireParticleSystem;
        fireParticleSystem.SetActive(false);
    }
}
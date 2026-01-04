using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class BloodCollisionEmitter : MonoBehaviour
{
    ParticleSystem mainParticleSystem;  // Your main blood particle system
    ParticleSystem splatterParticleSystem;  // The secondary particle system for splatter effects

    public Vector3 angleOffset;

    float t;

    public float percentageChance = 100;

    private void Awake()
    {
        splatterParticleSystem = GameObject.Find("Splatter Part").GetComponent<ParticleSystem>();
        mainParticleSystem = GetComponent<ParticleSystem>();

        if (TogglePref.GetValue("screen blood", true) && PlayerController.localPlayer.gameplayCamera != null)
            if (Vector3.Distance(transform.position, PlayerController.localPlayer.gameplayCamera.transform.position) < 6f)
            {
                HealthIndicator.Instance.AddBloodToScreen((5, 8), Random.Range(0, 150) < percentageChance ? 1 : 0);
            }
    }

    private void Update()
    {
        t -= Time.deltaTime;
    }

    void OnParticleCollision(GameObject other)
    {
        if (t > 0)
            return;

        if (Random.Range(0, 100) > percentageChance)
            return;

        t = .03f;

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int numCollisionEvents = mainParticleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            ParticleCollisionEvent collisionEvent = collisionEvents[i];
            Vector3 collisionPoint = collisionEvent.intersection;
            Vector3 collisionNormal = collisionEvent.normal;

            Quaternion surfaceRotation = Quaternion.LookRotation(collisionNormal);

            Quaternion randomSpin = Quaternion.AngleAxis(Random.Range(0f, 360f), surfaceRotation * collisionNormal);

            Quaternion finalRotation = surfaceRotation;

            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
            {
                position = collisionPoint,
                startSize = Random.Range(2f, 4f),
                startColor = Color.red,
                rotation3D = finalRotation.eulerAngles + angleOffset
            };

            splatterParticleSystem.Emit(emitParams, 1);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField] private float invincibilityTime = 0.4f;
    
    private HeadLeg player;

    private readonly Dictionary<HeadLeg, float> kickedEnemies = new Dictionary<HeadLeg, float>();

    private void Start()
    {
        player = GetComponentInParent<HeadLeg>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!player) return;
        
        HeadLeg enemy = other.transform.GetComponentInParent<HeadLeg>();
        
        if (player.StraighteningLeg)
        {
            player.rigidbody.AddForce(player.jumpForce * player.transform.up, ForceMode2D.Impulse);

            if (enemy)
            {
                if(other.gameObject.name == "Body")
                    enemy.rigidbody.AddForce(player.jumpForce * 0.5f * -other.contacts[0].normal, ForceMode2D.Impulse);
                
                if (kickedEnemies.ContainsKey(enemy))
                {
                    if (kickedEnemies[enemy] < Time.time - invincibilityTime)
                    {
                        kickedEnemies[enemy] = Time.time;
                        enemy.Kick();
                    }
                }
                else
                {
                    kickedEnemies.Add(enemy, Time.time);
                    enemy.Kick();
                }
            }
        }
    }
}

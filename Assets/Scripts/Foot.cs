using UnityEngine;

public class Foot : MonoBehaviour
{
    private HeadLeg player;

    private void Start()
    {
        player = GetComponentInParent<HeadLeg>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!player) return;
        
        HeadLeg enemy = other.transform.GetComponentInParent<HeadLeg>();
        
        if (!player.StraighteningLeg)
        {
            enemy?.Kick(player.baseDmg);
        }
        else
        {
            player.rigidbody.AddForce(player.jumpForce * player.transform.up, ForceMode2D.Impulse);
            
            enemy?.Kick(player.baseDmg * 2);
            enemy?.rigidbody.AddForce(player.jumpForce * player.transform.up, ForceMode2D.Impulse);
        }
    }
}

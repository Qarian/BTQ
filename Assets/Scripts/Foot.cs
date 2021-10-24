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
        
        if (player.StraighteningLeg)
        {
            player.rigidbody.AddForce(player.jumpForce * player.transform.up, ForceMode2D.Impulse);
            
            enemy?.Kick();
            if(other.gameObject.name == "Body")
                enemy?.rigidbody.AddForce(player.jumpForce * 0.5f * -other.contacts[0].normal, ForceMode2D.Impulse);
        }
    }
}

using UnityEngine;

public class Player : Character
{
    public override bool Checksanity()
    {
        throw new System.NotImplementedException();
    }

    public void IntializePlayer()
    {
        base.Intialize(100, 100);
    }

    public void OnHitWith(Enemy enemy)
    {
        LoseSanity(enemy.SanityHit);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnHitWith(enemy);
            Debug.Log($"{this.name} collider and lose Sanity {Sanity}");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntializePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;

    public int sanity;

    public IShootable Shooter;

    public abstract void Move();

    public abstract void OnHitWith(Character character);

    public void InitWeapon(int newDamage, IShootable newShooter, int newSanity)
    {
        damage = newDamage;
        Shooter = newShooter;
        sanity = newSanity;
    }

    public int GetShootDirection()
    {
        float value = Shooter.ShootPoint.position.x -
            Shooter.ShootPoint.parent.position.x;
        if (value > 0)
            return 1;
        else return -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            OnHitWith(other.GetComponent<Character>());
            Destroy(this.gameObject, 3f);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
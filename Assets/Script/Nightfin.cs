using UnityEngine;
using System.Collections.Generic;

public class Nightfin : Enemy, IShootable
{
    public GameObject Bullet2 { get; set; }
    public Transform ShootPoint2 { get; set; }

    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }

    public float ReloadTime { get; set; } = 1.0f;
    public float WaitTime { get; set; } = 0.0f;


    [SerializeField] float attackRange = 10.0f;

    public override void Behavior()
    {
         Player playerTarget = Player.Instance;
        Vector2 distance = transform.position - playerTarget.transform.position;
        if (distance.magnitude <= attackRange)
        {
            Shoot(); 
        }

    }

    public override bool Checksanity()
    {
        throw new System.NotImplementedException();
    }

    public void IntializeNightfin()
    {
        base.Intialize(50, 50);
    }

    public void Shoot()
    {
        if (WaitTime >= ReloadTime)
        {
            var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);

            SharkMagic magic = bullet.GetComponent<SharkMagic>();

            if (magic != null)
            {
                magic.InitWeapon(20, this, 10);
            }
            else
            {
                Debug.LogError("Bullet Prefab is missing the SharkMagic script component!");
            }

            WaitTime = 0;
        }
    }

    void Start()
    {
        IntializeNightfin();

        ReloadTime = 1.0f;
        WaitTime = 0.0f;

    }

    void Update()
    {
        WaitTime += Time.deltaTime;

        if (Player.Instance != null && Player.Instance.enabled)
        {
            Behavior(); 
        }
    }
}
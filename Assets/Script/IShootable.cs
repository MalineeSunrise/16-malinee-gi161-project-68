using UnityEngine;

public interface IShootable
{
    public GameObject Bullet { get; set; }

    public Transform ShootPoint { get; set; }

    public GameObject Bullet2 { get; set; }

    public Transform ShootPoint2 { get; set; }

    public float ReloadTime { get; set; }

    public float WaitTime { get; set; }

    //method
    public void Shoot();

}

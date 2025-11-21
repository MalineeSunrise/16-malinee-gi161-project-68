using UnityEngine;

public class Nightfin : Enemy
{
    public override void Behavior()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override bool Checksanity()
    {
        throw new System.NotImplementedException();
    }

    public void IntializeNightfin()
    {
        base.Intialize(50, 50);
    }

    void Start()
    {
        IntializeNightfin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

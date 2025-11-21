using UnityEngine;

public class Whispy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override bool Checksanity()
    {
        throw new System.NotImplementedException();
    }

    public void IntializeWhispy()
    {
        base.Intialize(70, 70);
    }

    void Start()
    {
        IntializeWhispy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

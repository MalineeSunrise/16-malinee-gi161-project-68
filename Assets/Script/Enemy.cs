using UnityEngine;
using System.Collections.Generic;

public abstract class Enemy : Character
{
    public int SanityHit { get; protected set; }
    public int DamageHit { get; protected set; }

    public abstract void Behavior();

    [System.Serializable]
    public class LootItem
    {
        public GameObject itemPrefabs;
        
        [Range(0, 100)] public float dropChance;
    }

    [Header("Enemy Loot Configuration")]
    [SerializeField]
    protected List<GameObject> guaranteedLoot = new List<GameObject>();

    [SerializeField]
    protected List<LootItem> lootTable = new List<LootItem>(); 

    private bool hasDroppedLoot = false;

    public void DropLoot()
    {
        foreach (GameObject loot in guaranteedLoot)
        {
            if (loot != null)
            {
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }

        foreach (LootItem item in lootTable)
        {
            if (item.itemPrefabs != null && Random.Range(0f, 100f) <= item.dropChance)
            {
                Instantiate(item.itemPrefabs, transform.position, Quaternion.identity);
            }
        }
    }

    void OnDestroy()
    {
        if (IsDead() && hasDroppedLoot == false)
        {
            hasDroppedLoot = true;
            DropLoot();
        }
    }
}
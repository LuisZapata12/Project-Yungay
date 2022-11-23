using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Range Item", menuName = "Inventory System/Items/Equipment/Range")]
public class EquipmentRange : EquipmentItem
{
    [HideInInspector]
    public float lastShootTime;
    public float shootDelay;
    [Range(0f, 0.9f)]
    public float bulletSpreadVarianceDis;
    public List<MunitonSlot> munitions = new();
    public int chargerNailsMax;
    public int chargerBulletsMax;
    [HideInInspector]
    public Vector2 bulletSpreadVariance;
    public float damage;
    [Range(1f, 50f), Min(1)]
    public float range;
    [Range(1f, 15f), Min(1)]
    public float zoom;
    public ParticleSystem spark;
    public Sprite look;
    public string shoot;
    public string reload;
    public string fail;

    private void Awake()
    {
        equipmentType = EquipmentType.Range;
        bulletSpreadVariance = new Vector2(bulletSpreadVarianceDis, bulletSpreadVarianceDis);
    }
}

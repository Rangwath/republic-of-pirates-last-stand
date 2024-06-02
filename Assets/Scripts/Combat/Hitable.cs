using System;
using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
{
    public static Action OnWoodHit;
    public static Action OnStoneHit;

    [SerializeField] private GameObject explosionPrefab;

    public void TakeHit(Vector2 hitPosition)
    {
        if (IsGameObjectInLayerMask(gameObject, GameManager.Instance.WoodImpactLayer))
        {
            OnWoodHit?.Invoke();
        }
        if (IsGameObjectInLayerMask(gameObject, GameManager.Instance.StoneImpactLayer))
        {
            OnStoneHit?.Invoke();
        }

        Instantiate(explosionPrefab, hitPosition, Quaternion.identity);
    }

    public bool IsGameObjectInLayerMask(GameObject gameObject, LayerMask layerMask)
    {
        // Convert the GameObject's layer to a bitmask and check if it matches the LayerMask
        return ((layerMask.value & (1 << gameObject.layer)) > 0);
    }
}

using UnityEngine;

public class DestroyUtil : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;

    public void DestroyThisGameObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (trailRenderer != null)
        {
            trailRenderer.transform.SetParent(null);
            Destroy(trailRenderer.gameObject, trailRenderer.time + 1f);
        }
    }
}

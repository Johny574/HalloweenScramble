


using UnityEngine;

public class Enemy : MonoBehaviour, IPoolObject<SpawnData>
{
    public bool Dead { get; set; } = false;
    public void Bind(SpawnData variant)
    {
        Dead = false;
        transform.position = variant.Position;
        transform.rotation = Quaternion.Euler(variant.Rotation);
        transform.localScale = variant.Scale;
        transform.SetParent(variant.Parent);
    }

    public void Kill()
    {
        Dead = true;
    }
}
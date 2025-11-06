using DG.Tweening;
using UnityEngine;

public class Candy : MonoBehaviour, IPoolObject<SpawnData>
{
    Pointer _pointer;

    public void AssignPointer(Pointer pointer)
    {
        _pointer = pointer;
    }


    void Start() {
        float startY = 0f;
        float endY = 1f;

        Sequence sequence = DOTween.Sequence();

        Tween moveTween = DOTween.To(() => startY, y =>
        {
            startY = y;
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        }, endY, 1f).SetLoops(-1, LoopType.Yoyo);

        // Rotation tween
        Tween rotateTween = transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Yoyo);

        // Chain them together
        sequence.Join(moveTween);
        sequence.Join(rotateTween);
    }

    
    public void Bind(SpawnData variant)
    {
        transform.position = variant.Position;
        transform.rotation = Quaternion.Euler(variant.Rotation);
        transform.localScale = variant.Scale;
        transform.SetParent(variant.Parent);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Player") || !CandyTracker.Instance.HasSpace())
            return;

        CandyTracker.Instance.CandyCollected?.Invoke();
        GameObject.Destroy(_pointer.gameObject);
        gameObject.SetActive(false);
    }
}

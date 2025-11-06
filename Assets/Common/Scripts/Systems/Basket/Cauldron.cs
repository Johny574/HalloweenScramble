using UnityEngine;

public class Cauldron : MonoBehaviour
{
    bool canDrop = false;
    [SerializeField] GameObject _pointer;


    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var pointer = GameObject.Instantiate(_pointer);
        pointer.GetComponent<Pointer>().Bind(player, new Vector3(transform.position.x , 1, transform.position.z));
    }
    
    public void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;

        canDrop = true;
    }

    public void OnTriggerExit(Collider col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;

        canDrop = false;
    }    

    void Update() {
        if (!canDrop)
            return;

        if (Input.GetKeyDown(KeyCode.F))
            CandyTracker.Instance.DropCandy();
    }    
}
using UnityEngine;

public class MeshSwitcher : MonoBehaviour
{
    [SerializeField] private Mesh[] meshOptions = new Mesh[] { };

    void Awake()
    {
        if (meshOptions.Length > 0)
        { GetComponent<MeshFilter>().mesh = meshOptions[Random.Range(0, meshOptions.Length)]; }
    }
}

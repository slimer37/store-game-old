using UnityEngine;

public class MeshSwitcher : MonoBehaviour
{
    [SerializeField] Mesh[] meshOptions = new Mesh[] { };

    void Awake()
    {
        if (meshOptions.Length > 0)
        {
            Mesh randomMesh = meshOptions[Random.Range(0, meshOptions.Length)];

            if (GetComponent<MeshFilter>())
            { GetComponent<MeshFilter>().mesh = randomMesh; }
            else
            { GetComponent<SkinnedMeshRenderer>().sharedMesh = randomMesh; }
        }
    }
}

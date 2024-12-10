using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;

public class RuntimeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navmeshSurface;
    void Start()
    {
        navmeshSurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavMesh);

    }

    public void BuildNavMesh()
    {
        StartCoroutine(BuildNavmeshRoutine());
    }
        public IEnumerator BuildNavmeshRoutine()
        {
            yield return new WaitForEndOfFrame();
            navmeshSurface.BuildNavMesh();
        }
}

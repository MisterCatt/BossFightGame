using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    protected GameObject ObjectPrefab;

    [SerializeField] protected int ObjectsToInstantiate = 10;
    [SerializeField]
    [Header("Just info, no touch")]
    protected List<GameObject> ObjectsInPool;


    public GameObject GetObject()
    {
        if (!ObjectPrefab) return null;
        while (true)
        {
            foreach (var objectInPool in ObjectsInPool.Where(objectInPool => !objectInPool.activeSelf))
            {
                return objectInPool;
            }

            InstantiateObjects();
        }
    }

    public void InstantiateObjects()
    {
        if (!ObjectPrefab) return;

        for (int i = 0; i < ObjectsToInstantiate; i++)
        {
            var projectile = Instantiate(ObjectPrefab, transform);
            ObjectsInPool.Add(projectile);
            projectile.SetActive(false);
        }
    }

    public void DisableAllActiveObjects()
    {
        if (!ObjectPrefab) return;

        foreach (var objectInPool in ObjectsInPool.Where(objectInPool => objectInPool.activeSelf))
        {
            objectInPool.SetActive(false);
        }
    }
}

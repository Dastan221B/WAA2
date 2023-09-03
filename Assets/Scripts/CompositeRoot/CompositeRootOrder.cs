using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRootOrder : MonoBehaviour
{
    [SerializeField] private List<CompositeRoot> _compositeRoots;


    private void Awake()
    {
        foreach(var composite in _compositeRoots)
        {
            try
            {
                composite.Composite();
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
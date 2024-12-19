using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected bool _init = false;

    public virtual bool Init()
    {
        if (_init)
            return false;

        return _init = true;
    }

    private void Start()
    {
        Init();
    }
}

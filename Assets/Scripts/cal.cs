using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cal : MonoBehaviour
{
    [SerializeField]
    private int count;

    public int Count
    {
        get { return count; }
        set { count = value;  }
    }


}

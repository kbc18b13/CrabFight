using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaniGenerator : MonoBehaviour
{
    private static int KaniKazu = 2;

    public static void SetKaniKazu(int kazu)
    {
        KaniKazu = kazu;
    }

    void Awake()
    {
        
    }
}

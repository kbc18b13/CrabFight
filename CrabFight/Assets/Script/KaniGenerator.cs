using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaniGenerator : MonoBehaviour {
    private static int KaniKazu = 2;

    public static void SetKaniKazu(int kazu)
    {
        KaniKazu = kazu;
    }

    void Awake()
    {
        Vector3 pos = new Vector3(0, 2.5f, 5.0f);
        Quaternion kakudo = new Quaternion();
        if (KaniKazu > 1)
        {
            kakudo = Quaternion.Euler(0, 360.0f / KaniKazu - 1, 0);
        }
        for (int i = 0; i < KaniKazu; i++)
        {
            GameObject kani = (GameObject)Resources.Load("kani Variant");

            pos = kakudo * pos;

            Quaternion rot = new Quaternion();
            Vector3 vec = -pos;
            vec.y = 0;
            rot.SetLookRotation(vec);

            Instantiate(kani, pos, rot);
        }

        Destroy(gameObject);
    }
}

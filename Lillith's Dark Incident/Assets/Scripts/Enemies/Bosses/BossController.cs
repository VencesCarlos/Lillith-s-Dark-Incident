using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : FloeraStatesController
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

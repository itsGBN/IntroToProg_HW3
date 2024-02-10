using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipBlock()
    {
        cube.transform.rotation = Quaternion.Slerp(cube.transform.rotation, Quaternion.Euler(0,180,0), 0.2f );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源点行为
/// </summary>
public class ResourceAction : MonoBehaviour
{
    public GameObject Drops;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void beDestroy()
    {
        Destroy(gameObject);
        GameObject drop = Instantiate(Drops, transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_inPoints : MonoBehaviour
{
    public application_data app_data;
    public List<Vector3> in_positions;
    // Start is called before the first frame update
    void Start()
    {
       
        for (int i = 0; i < in_positions.Count; i++)
        {
            GameObject add = new GameObject();        
            add.transform.position = in_positions[i];
            app_data.inPoints.Add(add.transform);
        }
       
        
    }
}

using JetBrains.Annotations;
using OpenCVForUnity.CoreModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class application_data : MonoBehaviour
{
    public List<Transform> inPoints;
    public Transform[] before_kabsch;
    public List<Transform> refPoints;
    public GameObject final_point;
    public List<double> P_points_list;
    public List<double> M_points_list;
    public List<Vector3> transformed_cloud;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using Supercluster.KDTree;
using OpenCVForUnity.CoreModule;

public class Execute_ICP : MonoBehaviour
{
    public application_data app_data;
    public double threshold;
    public int max_iterations;
    
    public void call_ICP()
    {
        print("app_data.M_points_list : " + app_data.M_points_list.Count);
        
        var m = Matrix<double>.Build;

        Matrix<double> M_Points = m.Dense(3, app_data.M_points_list.Count / 3);
        Matrix<double> P_Points = m.Dense(3, app_data.P_points_list.Count / 3);

        //print(M_array.Length);
        for (int i = 0; i < app_data.transformed_cloud.Count; i++)
        {
            double[] curr_row = new double[3];
            curr_row[0] = app_data.transformed_cloud[i].x;
            curr_row[1] = app_data.transformed_cloud[i].y; 
            curr_row[2] = app_data.transformed_cloud[i].z; 
            M_Points.SetColumn(i, curr_row);
        }

        for (int i = 0; i < app_data.P_points_list.Count / 3; i++)
        {
            double[] curr_row = new double[3];
            curr_row[0] = app_data.P_points_list[i * 3 + 0];
            curr_row[1] = app_data.P_points_list[i * 3 + 1];
            curr_row[2] = app_data.P_points_list[i * 3 + 2];
            P_Points.SetColumn(i, curr_row);
        }

        //print(M_Points.RowCount);
        Matrix<double> modified_mat = ICP.ICP_run(P_Points, M_Points, threshold, max_iterations, true);

        double[] modified_arr = M_Points.ToColumnMajorArray();

        app_data.M_points_list.Clear();
        app_data.M_points_list = modified_arr.ToList();
    }
}

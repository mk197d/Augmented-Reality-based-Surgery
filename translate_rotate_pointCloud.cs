using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class Translate_rotate_pointCloud : MonoBehaviour
{
    public application_data app_data;
    public GameObject initialRegistration;
    public GameObject finalRegistration;

    Vector3 translation = new Vector3(1.0f, 2.0f, 3.0f); // Translation vector
    Quaternion rotationQuaternion = Quaternion.Euler(0.0f, 45.0f, 0.0f); // Rotation quaternion

    public void modify_cloud()
    {
        translation = app_data.final_point.transform.position;
        translation.x /= (float)0.3611;
        translation.y /= (float)0.3611;
        translation.z /= (float)0.3611;

        rotationQuaternion = app_data.final_point.transform.rotation;
        app_data.transformed_cloud = new List<Vector3>();

        // Translate and rotate the point cloud
        TranslateAndRotatePointCloud();

        initialRegistration.SetActive(false);
        finalRegistration.SetActive(true);
    }

    void TranslateAndRotatePointCloud()
    {
        for (int i = 0; i < app_data.M_points_list.Count; i += 3)
        {
            double x = app_data.M_points_list[i];
            double y = app_data.M_points_list[i + 1];
            double z = app_data.M_points_list[i + 2];

            // Translate
            Vector3 translatedPoint = new Vector3((float)x, (float)y, (float)z) + translation;

            // Rotate
            Vector3 rotatedPoint = rotationQuaternion * translatedPoint;

            app_data.transformed_cloud.Add(rotatedPoint);
        }
    }
}

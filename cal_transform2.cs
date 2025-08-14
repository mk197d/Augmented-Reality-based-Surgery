using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class cal_transform2 : MonoBehaviour
{
    // Initial and final coordinates of points
    public application_data app_data;
    public GameObject bone_model;
    Vector3[] initialPoints;
    Vector3[] finalPoints;


    public void apply_final_registration()
    {

        initialPoints = new Vector3[app_data.transformed_cloud.Count];
        finalPoints = new Vector3[app_data.M_points_list.Count / 3];

        for (int i = 0; i < app_data.transformed_cloud.Count; i++)
        {
            finalPoints[i] = new Vector3((float)app_data.M_points_list[3 * i], (float)app_data.M_points_list[3 * i + 1], (float)app_data.M_points_list[3 * i + 2]);
            initialPoints[i] = new Vector3(app_data.transformed_cloud[i].x, app_data.transformed_cloud[i].y, app_data.transformed_cloud[i].z);

        }


        // Check if the number of initial and final points match
        if (initialPoints.Length != finalPoints.Length)
        {
            Debug.LogError("Number of initial and final points must be the same");
            return;
        }

        // Calculate average translation and rotation
        Vector3 averageTranslation = CalculateAverageTranslation();
        Quaternion averageRotation = CalculateAverageRotation();

        // Apply transformation to the object
        app_data.final_point.transform.rotation *= averageRotation;
        app_data.final_point.transform.position += averageTranslation;
        print(averageRotation.eulerAngles);

        //averageTranslation = new Vector3((float)(0.29), (float)(-0.5), (float)(-0.09));
        print(averageTranslation);

        Quaternion old_rotation = bone_model.transform.rotation;
        print(bone_model.transform.position);
        bone_model.transform.position -= averageTranslation;
        print(bone_model.transform.position);
        //bone_model.transform.position = new Vector3((float)5.75, (float)-22.11, (float)-2.04);
        //bone_model.transform.rotation.eulerAngles.Set(averageRotation.eulerAngles.x + old_rotation.eulerAngles.x, averageRotation.eulerAngles.y + old_rotation.eulerAngles.y, averageRotation.eulerAngles.z + old_rotation.eulerAngles.z);
    }

    Vector3 CalculateAverageTranslation()
    {
        Vector3 averageTranslation = Vector3.zero;

        for (int i = 0; i < initialPoints.Length; i++)
        {
            averageTranslation += finalPoints[i] - initialPoints[i];
        }

        // Calculate the average translation
        averageTranslation /= initialPoints.Length;

        float conversion_factor = (float)0.3611;
        averageTranslation.x *= conversion_factor;
        averageTranslation.y *= conversion_factor;
        averageTranslation.z *= conversion_factor;

        return averageTranslation;
    }

    Quaternion CalculateAverageRotation()
    {
        Quaternion averageRotation = Quaternion.identity;

        for (int i = 0; i < initialPoints.Length; i++)
        {
            // Calculate the rotation quaternion between initial and final points
            Quaternion rotationDelta = Quaternion.FromToRotation(initialPoints[i], finalPoints[i]);

            // Accumulate rotations
            averageRotation = rotationDelta * averageRotation;
        }

        // Calculate the average rotation
        averageRotation = Quaternion.SlerpUnclamped(Quaternion.identity, averageRotation, 1f / initialPoints.Length);

        return averageRotation;
    }
}

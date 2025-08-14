using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class calculate_transform : MonoBehaviour
{
    // Initial and final coordinates of points
    public application_data app_data;
    public GameObject bone_model;
    Vector3[] initialPoints;
    Vector3[] finalPoints;
    

    public void apply_registration()
    {
        
        initialPoints= new Vector3[app_data.inPoints.Count];
        finalPoints = new Vector3[app_data.inPoints.Count];

        for(int i =0; i < app_data.inPoints.Count; i++)
        {
            initialPoints[i] = new Vector3(app_data.inPoints[i].transform.position.x, app_data.inPoints[i].transform.position.y, app_data.inPoints[i].transform.position.z);
            finalPoints[i] = new Vector3(app_data.before_kabsch[i].transform.position.x, app_data.before_kabsch[i].transform.position.y, app_data.before_kabsch[i].transform.position.z);
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
        app_data.final_point.transform.rotation = averageRotation;
        app_data.final_point.transform.position = averageTranslation;
        print(averageRotation.eulerAngles);

        //averageTranslation = new Vector3((float)(-0.31), (float)(-0.59), (float)(-0.19));
        print(averageTranslation);

        Quaternion old_rotation = bone_model.transform.rotation;
        print(bone_model.transform.position);
        bone_model.transform.position -= averageTranslation;
        print(bone_model.transform.position);
        //bone_model.transform.position = new Vector3((float)5.46, (float)-21.61, (float)-1.95);
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

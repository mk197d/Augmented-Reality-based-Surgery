using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class Read_M_points : MonoBehaviour
{
    public string fileName = "Mesh.txt"; // Name of your .txt file
    public application_data app_data;

    void Start()
    {
        // Load data from file
        LoadData();
    }

    void LoadData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        try
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Parse each line and split values by space
            foreach (string line in lines)
            {
                string[] values = line.Split(' ');

                // Ensure there are exactly three values on each line
                if (values.Length == 3)
                {
                    // Parse each value as a double and add it to the array
                    for (int i = 0; i < 3; i++)
                    {
                        if (double.TryParse(values[i], out double value))
                        {
                            app_data.M_points_list.Add(value);
                        }
                        else
                        {
                            Debug.LogWarning($"Failed to parse value: {values[i]} on line: {line}");
                            return;
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid line format: {line}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading data: {e.Message}");
        }
    }
}

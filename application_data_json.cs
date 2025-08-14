using OpenCVForUnity.CoreModule;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class JSONSaveExample : MonoBehaviour
{
    public class application_data_json
    {
        public List<double> ix_cords;
        public List<double> iy_cords;
        public List<double> iz_cords;
        public List<double> i_scale;

        public List<double> rx_cords;
        public List<double> ry_cords;
        public List<double> rz_cords;
        public List<double> r_scale;

        public List<double> P_points_list;
        public List<double> M_points_list;

        public Vector3 position_change;
        public quaternion rotation_change;
    }

    public application_data online_data;
    application_data_json app_data;
    string saveFilePath;

    void Start()
    {
        app_data = new application_data_json();

        app_data.ix_cords= new List<double>();
        app_data.iy_cords= new List<double>();
        app_data.iz_cords= new List<double>();
        app_data.i_scale= new List<double>();

        app_data.rx_cords = new List<double>();
        app_data.ry_cords = new List<double>();
        app_data.rz_cords = new List<double>();
        app_data.r_scale = new List<double>();

        app_data.position_change = new Vector3();
        app_data.rotation_change= new quaternion();

        app_data.P_points_list = new List<double>();
        app_data.M_points_list = new List<double>();

        saveFilePath = Application.persistentDataPath + "/app_data.json";
        
    }

    public void SaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");

        
        app_data.ix_cords.Clear();
        app_data.iy_cords.Clear();
        app_data.iz_cords.Clear();
        app_data.i_scale.Clear();
        print(online_data.inPoints.Count);
        for (int i = 0; i < online_data.inPoints.Count; i++)
        {
            app_data.ix_cords.Add(online_data.inPoints[i].position.x);
            app_data.iy_cords.Add(online_data.inPoints[i].position.y);
            app_data.iz_cords.Add(online_data.inPoints[i].position.z);
            app_data.i_scale.Add(online_data.inPoints[i].localScale.x);
        }

        for (int i = 0; i < online_data.refPoints.Count; i++)
        {
            app_data.rx_cords.Add(online_data.refPoints[i].position.x);
            app_data.ry_cords.Add(online_data.refPoints[i].position.y);
            app_data.rz_cords.Add(online_data.refPoints[i].position.z);
            app_data.r_scale.Add(online_data.refPoints[i].localScale.x);
        }

        app_data.M_points_list = online_data.M_points_list;
        app_data.P_points_list = online_data.P_points_list;
        app_data.position_change = new Vector3(online_data.final_point.transform.position.x, online_data.final_point.transform.position.y, online_data.final_point.transform.position.z) ;
        app_data.rotation_change = new quaternion(online_data.final_point.transform.rotation.x, online_data.final_point.transform.rotation.y, online_data.final_point.transform.rotation.z, online_data.final_point.transform.rotation.w);

        string savePlayerData = JsonUtility.ToJson(app_data);
        print(savePlayerData);
        
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }
    

    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            app_data = JsonUtility.FromJson<application_data_json>(loadPlayerData);

            online_data.inPoints = new List<Transform>();
            print(app_data.ix_cords.Count);
            for (int i = 0; i < app_data.ix_cords.Count; i++)
            {
                GameObject addition = new GameObject();
                Vector3 add_position = new Vector3((float)app_data.ix_cords[i], (float)app_data.iy_cords[i], (float)app_data.iz_cords[i]);
                addition.transform.position = add_position;
                addition.transform.localScale = new Vector3((float)app_data.i_scale[i], (float)app_data.i_scale[i], (float)app_data.i_scale[i]);
                online_data.inPoints.Add(addition.transform);                
            }
            print(online_data.inPoints.Count);

            online_data.refPoints = new List<Transform>();
            for (int i = 0; i < app_data.rx_cords.Count; i++)
            {
                GameObject addition = new GameObject();
                Vector3 add_position = new Vector3((float)app_data.rx_cords[i], (float)app_data.ry_cords[i], (float)app_data.rz_cords[i]);
                addition.transform.position = add_position;
                addition.transform.localScale = new Vector3((float)app_data.r_scale[i], (float)app_data.r_scale[i], (float)app_data.r_scale[i]);
                online_data.refPoints.Add(addition.transform);
            }
            
            //online_data.M_points_list = new List<double>();
            
            //online_data.M_points_list = app_data.M_points_list;
            online_data.P_points_list = app_data.P_points_list;

            GameObject add_pos = new GameObject();
            add_pos.transform.position = new Vector3((float)app_data.position_change.x, (float)app_data.position_change.y, (float)app_data.position_change.z);
            add_pos.transform.rotation = new quaternion();
            add_pos.transform.rotation = app_data.rotation_change;
            online_data.final_point = add_pos;

            Debug.Log("Load game complete! \ninPoints: " + ")");
        }
        else
            Debug.Log("There is no save files to load!");

    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }

    public void NewGame()
    {
        
        //app_data.position = Vector3.zero;

            Debug.Log("Started from beginning! \ninPoints: " + ")");
    }

    public void ChangeData()
    {
        
        // app_data.position = new Vector3(4, 5, 6);

        Debug.Log("Data has been updated! \ninPoints: " + ")");
    }
}
using System;
using System.Collections.Generic;
using UnityEditor;
using Newtonsoft.Json;
using UnityEngine;
using Game.Data;

namespace Game
{
    [Serializable]
    public struct GameData
    {
        public List<Course> CourseList;
        public Course GetCourse(CourseCode code)
        {
            foreach (var course in this.CourseList)
            {
                if (course.Code == code)
                    return course;
            }
            Debug.Log($"Failed to find Course : {code}");
            return new Course();
        }
    }

    public class GameDataLoader : MonoBehaviour
    {
        [SerializeField] private TextAsset courseJsonFile;
        [SerializeField] private List<Course> courseList;
        [HideInInspector] public GameData GameData = new GameData();

        public void LoadAll()
        {
            LoadCourse();

            this.GameData.CourseList = courseList;
            Debug.Log("Load Course Complete!");
        }

        public void LoadCourse()
        {
            if (courseJsonFile != null)
                this.courseList = JsonConvert.DeserializeObject<List<Course>>(courseJsonFile.text);
            else
                Debug.Log("Failed to Load Game Data : No courseJsonFile");
        }
    }

    [CustomEditor(typeof(GameDataLoader))]
    public class GameDataLoaderEditor : Editor
    {
        private GameDataLoader loader;

        private void OnEnable()
        {
            loader = target as GameDataLoader;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Load All"))
            {
                loader.LoadAll();
            }
        }
    }
}
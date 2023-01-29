#if UNITY_EDITOR
using UnityEngine;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using Game.Data;

namespace Tools
{
    [CustomEditor(typeof(CSVConverter))]
    public class CSVConverterEditor : Editor
    {
        private CSVConverter csvConverter;
        private void OnEnable()
        {
            csvConverter = target as CSVConverter;
        }

        public override void OnInspectorGUI()
        {
            csvConverter.CSVData = (TextAsset)EditorGUILayout.ObjectField("변환하고자 하는 csv 데이터 파일", csvConverter.CSVData, typeof(TextAsset), true);
            csvConverter.FileName = EditorGUILayout.TextField("변환된 json 파일명", csvConverter.FileName);
            csvConverter.DataType = (CSVDataType)EditorGUILayout.EnumPopup("데이터 타입", csvConverter.DataType);
            if (GUILayout.Button("Convert CSV to Json"))
                ConvertCSVToJson();
            if (GUILayout.Button("Data Valid Test"))
                DoValidTest();
        }

        private void ConvertCSVToJson()
        {
            switch (csvConverter.DataType)
            {
                case CSVDataType.CourseData:
                    ConvertCourseData();
                    break;
            }
        }

        private void DoValidTest()
        {
            var data = CSVReader.ReadToString(csvConverter.CSVData);
            var everyDataValid = true;
            switch (csvConverter.DataType)
            {
                case CSVDataType.CourseData:
                    foreach (var dict in data)
                    {
                        if (!IsCourseDataValid(dict))
                            everyDataValid = false;
                    }
                    break;
            }

            if (everyDataValid)
                Debug.Log("Valid Test End: Every Data is Valid");
            else
                Debug.Log("Valid Test End: Some Data are inValid");
        }

        private void ConvertCourseData()
        {
            var data = CSVReader.ReadToString(csvConverter.CSVData);
            var courseDataList = new List<Course>();
            var everyDataValid = true;

            foreach (var dict in data)
            {
                courseDataList.Add(CourseBy(dict, out bool isValid));
                if (!isValid)
                    everyDataValid = false;
            }

            if (everyDataValid)
            {
                var saveData = JsonConvert.SerializeObject(courseDataList);
                File.WriteAllText(Application.dataPath + $"/Data/{csvConverter.FileName}.json", saveData);
                Debug.Log("Conversion Succeed : Every Data is Valid");
                Debug.Log($"{csvConverter.FileName}.json is created in {Application.dataPath + "/Data"}");
            }
            else
                Debug.Log("Conversion Failed : Some Data are inValid");
        }

        private static Course CourseBy(Dictionary<string, string> dict, out bool isValid)
        {
            var keys = new List<string>(dict.Keys);
            var result = new Course();
            isValid = true;
            foreach (var key in keys)
            {
                switch (key)
                {
                    case "Code":
                        if (Enum.TryParse(dict[key], out CourseCode code))
                            result.Code = code;
                        else
                        {
                            Debug.Log($"Course Code of {dict["Name"]} is not Valid : {dict[key]}");
                            if ((Enum.TryParse(dict["Dept"], out Departments department)))
                                Debug.Log($"Recommended Value of {dict["Name"]} : " + ((int)department).ToString() + dict["Code"].Substring(dict["Code"].Length - 3));
                            isValid = false;
                        }

                        break;

                    case "Name":
                        result.Name = dict[key];
                        break;

                    case "Description":
                        result.Description = dict[key];
                        break;

                    case "Type":
                        var type = Course.GetType(dict[key]);
                        if (type != CourseType.None)
                            result.Type = type;
                        else if (Enum.TryParse(dict[key], out type))
                            result.Type = type;
                        else
                        {
                            Debug.Log($"Course Type of {dict["Name"]} is not Valid : {dict[key]}");
                            isValid = false;
                        }
                        break;

                    case "Dept":
                        var dept = Course.GetDept(dict[key]);
                        if (dept != Departments.None)
                            result.Dept = dept;
                        else if (Enum.TryParse(dict[key], out dept))
                            result.Dept = dept;
                        else
                        {
                            Debug.Log($"Course Department of {dict["Name"]} is not Valid : {dict[key]}");
                            isValid = false;
                        }
                        break;

                    case "Skill":
                        if (Enum.TryParse(dict[key], out SkillCode skillCode))
                            result.Skill = skillCode;
                        else
                        {
                            Debug.Log($"Course SkillCode of {dict["Name"]} is not Valid : {dict[key]}");
                            isValid = false;
                        }
                        break;

                    case "Difficulty":
                        if (int.TryParse(dict[key], out int difficulty))
                            result.Difficulty = difficulty;
                        else
                        {
                            Debug.Log($"Course Difficulty of {dict["Name"]} is not Valid : {dict[key]}");
                            isValid = false;
                        }
                        break;

                    case "Prerequisites":
                        if (dict[key] == "None")
                            break;

                        var strings = dict[key].Split(',');
                        var codeList = new List<CourseCode>();
                        foreach (var str in strings)
                        {
                            var str_trim = str.Trim();
                            if (Enum.TryParse(str_trim, out code))
                                codeList.Add(code);
                            else
                            {
                                Debug.Log($"Prerequisite Course Code of {dict["Name"]} is not Valid : {str_trim}");
                                isValid = false;
                            }
                        }
                        result.Prerequisites = codeList;
                        break;

                    default:
                        break;
                }
            }
            return result;
        }

        private static bool IsCourseDataValid(Dictionary<string, string> dict)
        {
            var keys = new List<string>(dict.Keys);
            var isValid = true;
            foreach (var key in keys)
            {
                switch (key)
                {
                    case "Code":
                        if (!Enum.TryParse(dict[key], out CourseCode code))
                        {
                            Debug.Log($"Course Code of {dict["Name"]} is not Valid : {dict[key]}");
                            if ((Enum.TryParse(dict["Dept"], out Departments department)))
                                Debug.Log($"Recommended Value of {dict["Name"]} : " + ((int)department).ToString() + dict["Code"].Substring(dict["Code"].Length - 3));
                            isValid = false;
                        }
                        break;

                    case "Type":
                        var type = Course.GetType(dict[key]);
                        if (type == CourseType.None && !Enum.TryParse(dict[key], out type))
                        {
                            Debug.Log($"Course Type of {dict["Name"]} is not Valid : {dict[key]}");
                            isValid = false;
                        }
                        break;

                    case "Dept":
                        var dept = Course.GetDept(dict[key]);
                        if (dept == Departments.None || !Enum.TryParse(dict[key], out dept))
                        {
                            Debug.Log($"Course Department of {dict["Name"]} is not Valid : {dict[key]}");
                            isValid = false;
                        }
                        break;

                    case "Skill":
                        if (!Enum.TryParse(dict[key], out SkillCode skillCode))
                        {
                            Debug.Log($"Course SkillCode of {dict["Name"]} is not Valid : {dict[key]}");
                            isValid = false;
                        }
                        break;

                    case "Difficulty":
                        if (!int.TryParse(dict[key], out int difficulty))
                        {
                            Debug.Log($"Course Difficulty of {dict["Name"]} is not Valid : {dict[key]}");
                            isValid = false;
                        }
                        break;

                    case "Prerequisites":
                        if (dict[key] == "None")
                            break;

                        var strings = dict[key].Split(',');
                        foreach (var str in strings)
                        {
                            var str_trim = str.Trim();
                            if (!Enum.TryParse(str_trim, out code))
                            {
                                Debug.Log($"Prerequisite Course Code of {dict["Name"]} is not Valid : {str_trim}");
                                isValid = false;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            return isValid;
        }
    }
}

#endif
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public enum CourseType
    {
        None = 0,
        BasicRequired = 1, // 기초필수
        BasicElective = 2, // 기초선택
        MajorRequired = 3, // 전공필수
        MajorElective = 4, // 전공선택
        MandatoryGeneral = 5, // 교양필수
        HumanitiesAndSocialElective = 6, // 인문사회선택
        GeneralRequired = 7, // 공통필수
        GraduateElective = 8, // 선택(석/박사)
        OtherElective = 9, // 자유선택
    }

    [Serializable]
    public class Course
    {
        public CourseCode Code;
        public string Name;
        public string Description;
        public int Credit;
        public CourseType Type;
        public Departments Dept;
        public SkillCode Skill;
        public bool canTake;
        public int Difficulty;
        public List<CourseCode> Prerequisites;
    }

    [CreateAssetMenu(fileName = "CourseData", menuName = "Data/Course")]
    public class CourseData : ScriptableObject
    {
        public List<Course> courseList;
    }
}
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

    public enum Departments
    {
        None = 0,
        HSS = 1, // 인문사회과학과정 (인문)
        AE = 2, // 항공우주공학과 (항공)
        BS = 3, // 생명과학과 (생명)
        BiS = 4, // 바이오및뇌공학과 (바공)
        CBE = 5, // 생명화학공학과 (생화공)
        CE = 6, // 건설및환경공학과 (건환)
        CH = 7, // 화학과 (화학)
        CS = 8, // 전산학부 (전산)
        EE = 9, // 전기및전자공학부 (전자)
        ID = 10, // 산업디자인학과 (산디)
        IE = 11, // 산업및시스템공학과 (산공)
        MAS = 12, // 수리과학과 (수리)
        ME = 13, // 기계공학과 (기계)
        MS = 14, // 신소재공학과 (신소재)
        MSB = 15, // 기술경영학과 (기경)
        NQE = 16, // 원자력 및 양자공학과 (원양)
        PH = 17, // 물리학과 (물리)
        TS = 18 // 융합인재학부 (융인)
    }

    [Serializable]
    public struct Course
    {
        public CourseCode Code;
        public string Name;
        public string Description;
        public int Credit;
        public CourseType Type;
        public Departments Dept;
        public SkillCode Skill;
        public int Difficulty;
        public List<CourseCode> Prerequisites;

        public static CourseType GetType(string type)
        {
            switch (type)
            {
                case "기초필수":
                    return CourseType.BasicRequired;
                case "기초선택":
                    return CourseType.BasicElective;
                case "전공필수":
                    return CourseType.MajorRequired;
                case "전공선택":
                    return CourseType.MajorElective;
                case "교양필수":
                    return CourseType.MandatoryGeneral;
                case "인문사회선택":
                    return CourseType.HumanitiesAndSocialElective;
                case "공통필수":
                    return CourseType.GeneralRequired;
                case "선택(석/박사)":
                    return CourseType.GraduateElective;
                case "자유선택":
                    return CourseType.OtherElective;

                default:
                    return CourseType.None;
            }
        }

        public static Departments GetDept(string dept)
        {
            switch (dept)
            {
                case "인문":
                    return Departments.HSS;
                case "항공":
                    return Departments.AE;
                case "생명":
                    return Departments.BS;
                case "바공":
                    return Departments.BiS;
                case "생화공":
                    return Departments.CBE;
                case "건환":
                    return Departments.CE;
                case "화학":
                    return Departments.CH;
                case "전산":
                    return Departments.CS;
                case "전자":
                    return Departments.EE;
                case "산디":
                    return Departments.ID;
                case "산공":
                    return Departments.IE;
                case "수리":
                    return Departments.MAS;
                case "기계":
                    return Departments.ME;
                case "신소재":
                    return Departments.MS;
                case "기경":
                    return Departments.MSB;
                case "원양":
                    return Departments.NQE;
                case "물리":
                    return Departments.PH;
                case "융인":
                    return Departments.TS;

                default:
                    return Departments.None;

            }
        }
    }
}
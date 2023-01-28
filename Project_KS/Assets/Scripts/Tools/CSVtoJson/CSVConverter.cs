using UnityEngine;

namespace Tools
{
    public enum CSVDataType
    {
        None = 0,
        CourseData = 1,

    }

    [CreateAssetMenu(fileName = "CSVConverter", menuName = "Tools/CSVConverter")]
    public class CSVConverter : ScriptableObject
    {
        public TextAsset CSVData;
        public string FileName;
        public CSVDataType DataType;
    }
}
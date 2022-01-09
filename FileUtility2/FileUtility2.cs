using System.Runtime.CompilerServices;
using System.Text;

namespace FileUtility2
{
    public class FileUtility2
    {
        public static string GetTargetPath(string filePath, string targetParentDir)
        {
            var pathExcludingTopLevelDir = GetPathExcludingTopLevelDirectory(filePath);
            return Path.Combine(targetParentDir, pathExcludingTopLevelDir);
        }

        private static string GetPathExcludingTopLevelDirectory(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath))
            {
                return "";
            }
            // top level means have drive as parent
            var dirInfo = new DirectoryInfo(dirPath);
            if (dirInfo.Name.EndsWith(":\\") || dirInfo.Parent.Name.EndsWith(":\\"))
            {
                // this is the drive letter, don't need
                return "";
            }

            var recursiveResult = GetPathExcludingTopLevelDirectory(dirInfo.Parent.FullName);
            var pathSeparator = !string.IsNullOrEmpty(recursiveResult) ? "\\" : "";
            return recursiveResult + pathSeparator + dirInfo.Name;
        }

        private static string BuildPathFromReverseList(List<string> dirHierarchyReversed)
        {
            var pathAccumulated = "";
            for (var i = dirHierarchyReversed.Count - 1; i >= 0; i--)
            {
                pathAccumulated = Path.Combine(pathAccumulated, dirHierarchyReversed[i]);
            }

            return pathAccumulated;
        }
    }
}
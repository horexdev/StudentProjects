using System.Collections.Generic;
using StudentProjects.Database;

namespace StudentProjects
{
    public class Pools
    {
        public static List<Teacher> Teachers;
        public static List<Student> Students;

        public static bool IsPoolsNotLoaded()
        {
            return Teachers == null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProjects.Database
{
    public static class Context
    {
        /// <summary>
        /// Загружает всех учителей
        /// </summary>
        /// <returns></returns>
        public static async Task LoadTeachers()
        {
            using (var context = new StudentProjectsEntities())
            {
                var list = await context.Teachers.Include(t => t.AcademicDegree).Include(t => t.AcademicTitle).ToListAsync();
                Pools.Teachers = list;
            }
        }

        /// <summary>
        /// Возвращает список учеников
        /// </summary>
        /// <returns></returns>
        public static async Task LoadStudents()
        {
            using (var context = new StudentProjectsEntities())
            {
                var list = await context.Students.Include(t => t.Group).Include(t => t.Speciality).ToListAsync();
                Pools.Students = list;
            }
        }

        /// <summary>
        /// Возвращает список всех учёных степеней
        /// </summary>
        /// <returns></returns>
        public static List<AcademicDegree> GetAllAcademicDegrees()
        {
            using (var context = new StudentProjectsEntities())
            {
                var list = context.AcademicDegrees.ToList();

                return list;
            }
        }

        /// <summary>
        /// Возвращает список всех учёных званий
        /// </summary>
        /// <returns></returns>
        public static List<AcademicTitle> GetAllAcademicTitles()
        {
            using (var context = new StudentProjectsEntities())
            {
                var list = context.AcademicTitles.ToList();

                return list;
            }
        }

        /// <summary>
        /// Возвращает список всех групп
        /// </summary>
        /// <returns></returns>
        public static List<Group> GetAllGroups()
        {
            using (var context = new StudentProjectsEntities())
            {
                var list = context.Groups.ToList();

                return list;
            }
        }

        /// <summary>
        /// Возвращает список всех групп
        /// </summary>
        /// <returns></returns>
        public static List<Speciality> GetAllSpeciality()
        {
            using (var context = new StudentProjectsEntities())
            {
                var list = context.Specialities.ToList();

                return list;
            }
        }

        /// <summary>
        /// Добавляет сущность Teacher
        /// </summary>
        /// <param name="teacher"></param>
        public static void AddTeacher(Teacher teacher)
        {
            using (var context = new StudentProjectsEntities())
            {
                context.Teachers.Add(teacher);
                Pools.Teachers.Add(teacher);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Добавляет сущность Student
        /// </summary>
        /// <param name="student"></param>
        public static void AddStudent(Student student)
        {
            using (var context = new StudentProjectsEntities())
            {
                context.Students.Add(student);
                Pools.Students.Add(student);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Сохраняет сущность Teacher
        /// </summary>
        /// <param name="teacher"></param>
        public static void SaveTeacher(Teacher teacher)
        {
            using (var context = new StudentProjectsEntities())
            {
                context.Teachers.AddOrUpdate(teacher);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Проверяет любые совпадения сущности Teacher в БД
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static bool IsAnyTeacherCoincidences(string fullName)
        {
            using (var context = new StudentProjectsEntities())
            {
                return context.Teachers.Any(t => t.FullName == fullName);
            }
        }

        /// <summary>
        /// Проверяет любые совпадения сущности Student в БД
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static bool IsAnyStudentCoincidences(string fullName)
        {
            using (var context = new StudentProjectsEntities())
            {
                return context.Students.Any(s => s.FullName == fullName);
            }
        }
    }
}
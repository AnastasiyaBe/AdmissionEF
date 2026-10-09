using System;
using System.Linq;
using AdmissionEF.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmissionEF
{
    public static class Queries
    {
        // Пункт 2.1. Выборка всех данных из таблицы на стороне «один»
        // Таблица: Faculty
        public static void Query1_AllFaculties(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.1. Все факультеты (первые 5) ===");

            var faculties = db.Faculties.Take(5).ToList();

            foreach (var f in faculties)
                Console.WriteLine($"  {f.FacultyId}: {f.Name}");

            Console.WriteLine($"Всего факультетов: {db.Faculties.Count()}");
        }

        // Пункт 2.2. Фильтр из таблицы на стороне «один»
        // Таблица: Specialty, фильтр — очная форма + бюджетный план > 25 
        public static void Query2_FilteredSpecialties(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.2. Специальности: очная форма, бюджетный план > 25 (первые 5) ===");

            var specialties = db.Specialties
                .Where(s => s.EducationForm == "очная" && s.BudgetPlan > 25)
                .Take(5)
                .ToList();

            foreach (var s in specialties)
                Console.WriteLine($"  {s.Code} {s.Name} | бюджет: {s.BudgetPlan}");

            Console.WriteLine($"Найдено: {db.Specialties.Count(s => s.EducationForm == "очная" && s.BudgetPlan > 25)}");
        }

        // Пункт 2.3. Группировка с итогом из таблицы на стороне «многие»
        // Таблица: Application, группировка по SpecialtyId, счёт заявлений
        public static void Query3_ApplicationsBySpecialty(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.3. Количество заявлений по специальностям (топ-5) ===");

            var groups = db.Applications
                .GroupBy(a => a.SpecialtyId)
                .Select(g => new
                {
                    SpecialtyId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToList();

            foreach (var g in groups)
                Console.WriteLine($"  Специальность {g.SpecialtyId}: заявлений — {g.Count}");
        }
        
        // Пункт 2.4. Выборка двух полей из двух связанных таблиц
        // Specialty + Faculty (один-ко-многим)
        public static void Query4_SpecialtyAndFaculty(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.4. Специальность и её факультет (первые 5) ===");

            var rows = db.Specialties
                .Select(s => new
                {
                    s.Name,
                    Faculty = s.Faculty.Name
                })
                .Take(5)
                .ToList();

            foreach (var r in rows)
                Console.WriteLine($"  {r.Name} → {r.Faculty}");
        }

        // Пункт 2.5. Выборка из двух таблиц с фильтром
        // Application + Applicant, фильтр — средний балл >= 9.0
        public static void Query5_ApplicationsWithFilter(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.5. Заявления абитуриентов со средним баллом >= 9.0 (первые 5) ===");

            var rows = db.Applications
                .Where(a => a.Applicant.AverageScore >= 9.0m)
                .Select(a => new
                {
                    a.ApplicationNumber,
                    ApplicantName = a.Applicant.FullName,
                    ApplicantScore = a.Applicant.AverageScore,
                    a.Specialty.Name
                })
                .Take(5)
                .ToList();

            foreach (var r in rows)
                Console.WriteLine($"  {r.ApplicationNumber}: {r.ApplicantName} (балл {r.ApplicantScore}) → {r.Name}");
        }

        // Пункт 2.6. Вставка в таблицу на стороне «один»
        // Таблица: Faculty
        public static void Query6_InsertFaculty(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.6. Вставка нового факультета ===");

            var newFaculty = new Faculty
            {
                Name = "Тестовый факультет " + DateTime.Now.ToString("HHmmss")
            };

            db.Faculties.Add(newFaculty);
            db.SaveChanges();

            Console.WriteLine($"  Добавлен факультет: FacultyId = {newFaculty.FacultyId}, Name = {newFaculty.Name}");
        }

        // Пункт 2.7. Вставка в таблицу на стороне «многие»
        // Таблица: Specialty
        public static void Query7_InsertSpecialty(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.7. Вставка новой специальности ===");

            // Берём ID первого факультета из базы
            var firstFacultyId = db.Faculties.Select(f => f.FacultyId).First();

            var newSpecialty = new Specialty
            {
                Code = "TEST-" + DateTime.Now.ToString("HHmmss"),
                Name = "Тестовая специальность " + DateTime.Now.ToString("HHmmss"),
                FacultyId = firstFacultyId,
                EducationForm = "очная",
                DurationYears = 4.0m,
                BudgetPlan = 15,
                PaidPlan = 10
            };

            db.Specialties.Add(newSpecialty);
            db.SaveChanges();

            Console.WriteLine($"  Добавлена специальность: SpecialtyId = {newSpecialty.SpecialtyId}, Code = {newSpecialty.Code}");
        }

        // Пункт 2.8. Удаление из таблицы на стороне «один»
        // Таблица: Faculty — удаляем тот, что вставили в 2.6
        public static void Query8_DeleteFaculty(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.8. Удаление тестового факультета ===");

            var testFaculty = db.Faculties
                .Where(f => f.Name.StartsWith("Тестовый факультет"))
                .OrderByDescending(f => f.FacultyId)
                .FirstOrDefault();

            if (testFaculty == null)
            {
                Console.WriteLine("  Тестовый факультет не найден.");
                return;
            }

            db.Faculties.Remove(testFaculty);
            db.SaveChanges();

            Console.WriteLine($"  Удалён факультет: {testFaculty.Name}");
        }

        // Пункт 2.9. Удаление из таблицы на стороне «многие»
        // Таблица: Specialty — удаляем ту, что вставили в 2.7
        public static void Query9_DeleteSpecialty(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.9. Удаление тестовой специальности ===");

            var testSpecialty = db.Specialties
                .Where(s => s.Code.StartsWith("TEST-"))
                .OrderByDescending(s => s.SpecialtyId)
                .FirstOrDefault();

            if (testSpecialty == null)
            {
                Console.WriteLine("  Тестовая специальность не найдена.");
                return;
            }

            db.Specialties.Remove(testSpecialty);
            db.SaveChanges();

            Console.WriteLine($"  Удалена специальность: {testSpecialty.Name}");
        }

        // Пункт 2.10. Обновление записей по условию
        // Таблица: Application — первым 5 заявлениям меняем статус
        public static void Query10_UpdateApplications(AdmissionContext db)
        {
            Console.WriteLine("\n=== 2.10. Обновление статуса первых 5 заявлений ===");

            var apps = db.Applications
                .OrderBy(a => a.ApplicationId)
                .Take(5)
                .ToList();

            Console.WriteLine("  До обновления:");
            foreach (var a in apps)
                Console.WriteLine($"    {a.ApplicationNumber}: {a.Status}");

            foreach (var a in apps)
                a.Status = "принято";

            db.SaveChanges();

            Console.WriteLine("  После обновления:");
            foreach (var a in apps)
                Console.WriteLine($"    {a.ApplicationNumber}: {a.Status}");
        }
    }
}
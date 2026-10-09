# AdmissionEF
![Build](https://github.com/AnastasiyaBe/AdmissionEF/actions/workflows/build.yml/badge.svg)
Лабораторная работа №2 по дисциплине «Базы данных».

**Тема:** Использование Entity Framework и LINQ для работы с базами данных.

**Предметная область:** Приёмная комиссия вуза.

## Описание

Консольное .NET Core приложение, использующее Entity Framework Core для работы с базой данных MS SQL Server, созданной в лабораторной работе №1.

База данных: `db70106` (хостинг MonsterASP.net).

## Структура проекта

- `Models/` — классы сущностей и контекст данных (сгенерированы через Scaffold-DbContext);
- `Queries.cs` — 10 LINQ-запросов по заданию;
- `Program.cs` — точка входа;
- `appsettings.example.json` — пример строки подключения.

## Пункты задания

| № | Пункт | Метод |
|---|---|---|
| 2.1 | Выборка всех данных из таблицы на стороне «один» | `Query1_AllFaculties` |
| 2.2 | Выборка с фильтром из таблицы на стороне «один» | `Query2_FilteredSpecialties` |
| 2.3 | Группировка с итогом из таблицы на стороне «многие» | `Query3_ApplicationsBySpecialty` |
| 2.4 | Выборка двух полей из двух связанных таблиц | `Query4_SpecialtyAndFaculty` |
| 2.5 | Выборка из двух таблиц с фильтром | `Query5_ApplicationsWithFilter` |
| 2.6 | Вставка в таблицу на стороне «один» | `Query6_InsertFaculty` |
| 2.7 | Вставка в таблицу на стороне «многие» | `Query7_InsertSpecialty` |
| 2.8 | Удаление из таблицы на стороне «один» | `Query8_DeleteFaculty` |
| 2.9 | Удаление из таблицы на стороне «многие» | `Query9_DeleteSpecialty` |
| 2.10 | Обновление записей по условию | `Query10_UpdateApplications` |

## Запуск

1. Скопировать `appsettings.example.json` → `appsettings.json`.
2. Вписать свой пароль от БД.
3. `dotnet run`.

## Сборка

Проект собирается под Windows и Linux через GitHub Actions.
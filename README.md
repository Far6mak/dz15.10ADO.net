# Просмотр клиентов фитнес-клуба через Entity Framework Core

Консольное приложение на C# / .NET 8, которое читает данные из существующей таблицы `clients` базы PostgreSQL `Fitnes` с помощью Entity Framework Core и LINQ. SQL-запросы вручную не пишутся.

## Что делает программа

1. Выводит всех клиентов: ID, имя, фамилию и статус.
2. Выводит активных клиентов, отсортированных по фамилии.
3. Просит пользователя ввести ID и ищет соответствующего клиента.
4. Сообщает, если клиент с таким ID не найден.

Модель `Client` сопоставлена с таблицей `clients` и столбцами `client_id`, `first_name`, `last_name`, `status`.

## Требования

- .NET 8 SDK.
- PostgreSQL и уже созданная база `Fitnes` с заполненной таблицей `clients`.
- Учётные данные пользователя PostgreSQL, у которого есть право читать таблицу.

## Как клонировать и запустить

Клонируйте репозиторий и перейдите в папку проекта:

```bash
git clone https://github.com/Far6mak/dz15.10ADO.net.git
cd dz15.10ADO.net
```

Укажите строку подключения к своей базе данных. Пароль не нужно записывать в исходный код или отправлять в GitHub.

**Windows PowerShell:**

```powershell
$env:FITNESS_CONNECTION_STRING = "Host=localhost;Port=5432;Database=Fitnes;Username=postgres;Password=ВАШ_ПАРОЛЬ"
dotnet run --project FitnessClients.csproj
```

**Linux / macOS:**

```bash
export FITNESS_CONNECTION_STRING="Host=localhost;Port=5432;Database=Fitnes;Username=postgres;Password=ВАШ_ПАРОЛЬ"
dotnet run --project FitnessClients.csproj
```

Замените `localhost`, имя пользователя и пароль на значения своей установки PostgreSQL. Если база находится на другом сервере, укажите его адрес вместо `localhost`.

Пакет `Npgsql.EntityFrameworkCore.PostgreSQL` версии 8.0.11 подключён в файле проекта и совместим с .NET 8. Отдельно устанавливать EF Core не требуется.

## Примечание о статусах

Для списка активных клиентов программа распознаёт значения `active`, `активен`, `активна` и `активный` без учёта регистра. Если в вашей таблице используется другое значение статуса, измените условия фильтра в `Program.cs` под фактические данные.

using FitnessClients;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;

try
{
    using var db = new FitnessContext();

    // 1. Все клиенты.
    Console.WriteLine("=== Все клиенты ===");
    List<Client> clients = db.Clients
        .AsNoTracking()
        .OrderBy(client => client.ClientId)
        .ToList();

    if (clients.Count == 0)
    {
        Console.WriteLine("В таблице clients пока нет клиентов.");
    }
    else
    {
        foreach (Client client in clients)
            PrintClient(client);
    }

    // 2. Активные клиенты, отсортированные по фамилии.
    // Поддерживаются распространённые варианты статуса на русском и английском.
    Console.WriteLine();
    Console.WriteLine("=== Активные клиенты (по фамилии) ===");

    List<Client> activeClients = db.Clients
        .AsNoTracking()
        .Where(client =>
            client.Status.ToLower() == "active" ||
            client.Status.ToLower() == "активен" ||
            client.Status.ToLower() == "активна" ||
            client.Status.ToLower() == "активный")
        .OrderBy(client => client.LastName)
        .ThenBy(client => client.FirstName)
        .ToList();

    if (activeClients.Count == 0)
    {
        Console.WriteLine("Активные клиенты не найдены. Проверьте значения столбца status.");
    }
    else
    {
        foreach (Client client in activeClients)
            PrintClient(client);
    }

    // 3. Поиск клиента по введённому ID.
    Console.WriteLine();
    Console.Write("Введите ID клиента: ");
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int clientId))
    {
        Console.WriteLine("Ошибка: ID должен быть целым числом.");
        return;
    }

    // 4. Сообщение, если клиент с таким ID отсутствует.
    Client? foundClient = db.Clients
        .AsNoTracking()
        .FirstOrDefault(client => client.ClientId == clientId);

    Console.WriteLine();
    if (foundClient is null)
    {
        Console.WriteLine($"Клиент с ID {clientId} не найден.");
    }
    else
    {
        Console.WriteLine("=== Найденный клиент ===");
        PrintClient(foundClient);
    }
}
catch (InvalidOperationException ex) when (ex.Message.Contains("FITNESS_CONNECTION_STRING"))
{
    Console.WriteLine(ex.Message);
    Environment.ExitCode = 1;
}
catch (Exception ex)
{
    Console.WriteLine("Не удалось подключиться к базе данных или прочитать клиентов.");
    Console.WriteLine($"Подробности: {ex.Message}");
    Environment.ExitCode = 1;
}

static void PrintClient(Client client)
{
    Console.WriteLine(
        $"ID: {client.ClientId} | Имя: {client.FirstName} | Фамилия: {client.LastName} | Статус: {client.Status}");
}

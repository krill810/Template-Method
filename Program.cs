using System;
using System.Collections.Generic;

namespace OrderProcessingDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Print.Intdrodution();
            DemonstrateTemplateMethod();

            // интерактивное меню
            var manager = new OrderManager();
            bool running = true;

            while (running)
            {
                Print.Menu();

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ProcessNewOrder(manager, new RegularOrderProcessor(), "ОБЫЧНЫЙ");
                        break;

                    case "2":
                        ProcessNewOrder(manager, new PremiumOrderProcessor(), "ПРЕМИУМ");
                        break;

                    case "3":
                        manager.ShowAllOrders();
                        break;

                    case "4":
                        manager.ClearAllOrders();
                        Console.WriteLine($"Всего заказов: {manager.Count}");
                        break;

                    case "5":
                        running = false;
                        Console.WriteLine("Завершение работы программы.");
                        break;

                    default:
                        Console.WriteLine("Неверный пункт меню. Попробуйте снова.");
                        break;
                }
            }
        }
        static void ProcessNewOrder(OrderManager manager, OrderProcessor processor, string typeName)
        {
            while (true)
            {
                Console.Write("Введите сумму заказа: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount) )
                {
                    Console.WriteLine("Ошибка: Сумма заказа должна состоять из чисел.\n");
                    continue;
                }
                if (amount <= 0)
                {
                    Console.WriteLine("Ошибка: Сумма заказа должна быть положительной.\n");
                    continue;
                }
                try
                {
                    manager.ProcessOrder(processor, amount);
                    //Подтверждение после обработки
                    Console.WriteLine($"Заказ на сумму {amount} руб. обработан как {typeName}. Всего заказов: {manager.Count}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки: {ex.Message}");
                }
                break;
            }
        }

        //демонстрация работы паттерна
        static void DemonstrateTemplateMethod()
        {
            Console.WriteLine("----- ДЕМОНСТРАЦИЯ РАБОТЫ ПАТТЕРНА -----");
            Console.WriteLine("Клиентский код работает с разными типами заказов одинаково:\n");

            OrderProcessor regular = new RegularOrderProcessor();
            OrderProcessor premium = new PremiumOrderProcessor();

            Console.WriteLine("=> Обработка ОБЫЧНОГО заказа через базовый класс:");
            regular.ProcessOrder(1000);

            Console.WriteLine("\n=> Обработка ПРЕМИУМ-заказа через базовый класс:");
            premium.ProcessOrder(1000);

            Console.WriteLine("\n=> Вывод: одинаковый вызов ProcessOrder() — разное поведение шагов.");
            Console.WriteLine("----- КОНЕЦ ДЕМОНСТРАЦИИ -----\n");
        }
    }
}

// Вспомогательные классы

public class Print
{
    public static void Intdrodution()
    {
        Console.WriteLine(
            "Работу выполнили: Бедарева Софья и Сторожева Анна\n" +
            "Группа: 9ИС-435\n" +
            "Практическая работа №4: Реализация поведенческого паттерна «Шаблонный метод» в С#\n\n"
        );
    }

    public static void Menu()
    {
        Console.WriteLine(
            "\nВыберите действие:\n" +
            "1 - Оформить обычный заказ\n" +
            "2 - Оформить премиум-заказ\n" +
            "3 - Показать все заказы\n" +
            "4 - Очистить список заказов\n" +
            "5 - Выход\n"
        );
    }
}
public class OrderManager
{
    private readonly List<string> _orders = new List<string>();
    public int Count => _orders.Count;

    public void ProcessOrder(OrderProcessor processor, decimal amount)
    {
        processor.ProcessOrder(amount);
        _orders.Add($"Заказ на {amount} руб. [{processor.OrderType}]");
    }

    public void ShowAllOrders()
    {
        Console.WriteLine("\n--- Список выполненных заказов ---");
        if (_orders.Count == 0)
        {
            Console.WriteLine("Список пуст.");
            return;
        }
        for (int i = 0; i < _orders.Count; i++)
            Console.WriteLine($"{i + 1}. {_orders[i]}");
        Console.WriteLine($"Всего заказов: {_orders.Count}");
    }

    public void ClearAllOrders()
    {
        _orders.Clear();
        Console.WriteLine("Список заказов очищен.");
    }
}


// Абстрактный класс
public abstract class OrderProcessor
{
    public abstract string OrderType { get; }
    public abstract decimal DiscPercentage { get; }
    public void ProcessOrder(decimal amount)
    {
        ValidateOrder(amount);
        decimal discount = CalculateDiscount(amount);
        ProcessPayment(amount - discount);
        SendNotification();
        LogOrder();
    }

    protected virtual void ValidateOrder(decimal amount)
    {
        Console.WriteLine($"  [{OrderType}] Проверка заказа на сумму {amount} руб. ...");
        if (amount <= 0)
            throw new ArgumentException("Сумма заказа должна быть положительной.");
    }
    protected virtual decimal CalculateDiscount(decimal amount)
    {
        decimal discount = amount * DiscPercentage;
        Console.WriteLine($"  [{OrderType}] Скидка {DiscPercentage}: {discount} руб.");
        return discount;
    }
    protected virtual void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"  [{OrderType}] Оплата {amount} руб. Проведена.");
    }
    protected virtual void SendNotification()
    {
        Console.WriteLine($"  [{OrderType}] Уведомление: заказ принят в обработку.");
    }

    protected void LogOrder()
    {
        Console.WriteLine("Обработка заказа успешно завершена.");
    }
}

// Конкретные классы
// Обычный заказ
public class RegularOrderProcessor : OrderProcessor
{
    public override string OrderType => "Обычный";
    public override decimal DiscPercentage => 0m;
}

// премиум-заказ
public class PremiumOrderProcessor : OrderProcessor
{
    public override string OrderType => "Премиум";
    public override decimal DiscPercentage => 0.15m;

    protected override void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"  [{OrderType}] Оплата {amount} руб. проведена с приоритетом.");
    }
}

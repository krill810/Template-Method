using System;
using System.Collections.Generic;

namespace OrderProcessingDemo
{
    public abstract class OrderProcessor
    {
        public void ProcessOrder(decimal amount)
        {
            ValidateOrder(amount);
            decimal discount = CalculateDiscount(amount);
            ProcessPayment(amount - discount);
            SendNotification();
            LogOrder();
        }

        protected abstract void ValidateOrder(decimal amount);
        protected abstract decimal CalculateDiscount(decimal amount);
        protected abstract void ProcessPayment(decimal amount);
        protected abstract void SendNotification();
        public abstract string DisplayName {get;}

        protected void LogOrder()
        {
            Console.WriteLine("Обработка заказа успешно завершена.");
        }
    }

    //обычный заказ
    public class RegularOrderProcessor : OrderProcessor
    {
        protected override void ValidateOrder(decimal amount)
        {
            Console.WriteLine($"  [Обычный] Проверка заказа на сумму {amount} руб. ...");
            if (amount <= 0)
                throw new ArgumentException("Сумма заказа должна быть положительной.");
        }

        protected override decimal CalculateDiscount(decimal amount)
        {
            Console.WriteLine("  [Обычный] Скидка: 0%");
            return 0m;
        }

        protected override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"  [Обычный] Оплата {amount} руб. проведена.");
        }

        protected override void SendNotification()
        {
            Console.WriteLine("  [Обычный] Уведомление: заказ принят в обработку.");
        }

        public override string DisplayName => "Обычный";
    }

    // премиум-заказ
    public class PremiumOrderProcessor : OrderProcessor
    {
        protected override void ValidateOrder(decimal amount)
        {
            Console.WriteLine($"  [Премиум] Расширенная проверка заказа на сумму {amount} руб. ...");
            if (amount <= 0)
                throw new ArgumentException("Сумма заказа должна быть положительной.");
        }

        protected override decimal CalculateDiscount(decimal amount)
        {
            decimal discount = amount * 0.15m;
            Console.WriteLine($"  [Премиум] Скидка 15%: {discount} руб.");
            return discount;
        }

        protected override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"  [Премиум] Оплата {amount} руб. проведена с приоритетом.");
        }

        protected override void SendNotification()
        {
            Console.WriteLine("  [Премиум] Уведомление: премиум заказ принят в обработку.");
        }
        public override string DisplayName => "Премиум";
    }

    public class OrderManager
    {
        private readonly List<string> _orders = new List<string>();

        public void ProcessOrder(OrderProcessor processor, decimal amount)
        {
            processor.ProcessOrder(amount);
            _orders.Add($"Заказ на {amount} руб. [{processor.DisplayName}]");
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

        public int Count => _orders.Count;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine(
            "Работу выполнили: Бедарева Софья и Сторожева Анна\n" +
            "Группа: 9ИС-435\n" +
            "Практическая работа №4: Реализация поведенческого паттерна «Шаблонный метод» в С#\n\n"
        );

            DemonstrateTemplateMethod();

            // интерактивное меню
            var manager = new OrderManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n----- МЕНЮ -----");
                Console.WriteLine("1. Оформить обычный заказ");
                Console.WriteLine("2. Оформить премиум-заказ");
                Console.WriteLine("3. Показать все заказы");
                Console.WriteLine("4. Очистить список заказов");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите пункт: ");

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
            Console.Write("Введите сумму заказа: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное число.");
                return;
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
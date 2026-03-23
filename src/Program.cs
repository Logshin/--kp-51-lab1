using Sort;

class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        bool start = true;
        Sorter sorter = new Sorter();

        while (start)
        {
            Console.WriteLine("---Меню---");
            Console.WriteLine();
            Console.WriteLine("1 - додати новий елемент");
            Console.WriteLine("2 - видалити елемент за ID");
            Console.WriteLine("3 - вивести вміст колекції");
            Console.WriteLine("4 - заповнити контрольними значеннями");
            Console.WriteLine("5 - сортувати колекцію за спаданням");
            Console.WriteLine("6 - вивести статистику виконання алгортиму");
            Console.WriteLine("7 - очистити статистику виконання алгортиму");
            Console.WriteLine("8 - вивести топ 25% та кількість студентів по інтервалам");
            Console.WriteLine("0 - вихід");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case(1):
                    Record newRecord = sorter.CreateRecord();
                    sorter.AddRecord(newRecord);
                    Console.WriteLine("Елемент додано");
                    break;

                case(2):
                    Console.Write("Введіть ID студента для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int idToRemove))
                    {
                        sorter.RemoveRecord(idToRemove);
                    }
                    else
                    {
                        Console.WriteLine("Некоректний ID");
                    }
                    break;

                case(3):
                    sorter.PrintCollection();
                    break;

                case(4):
                    sorter.GenerateControlData();
                    break;

                case(5):
                    sorter.SortCollection();
                    break;

                case(6):
                    sorter.PrintStatistics();
                    break;

                case(7):
                    sorter.ClearStatistics();
                    break;

                case(8):
                    sorter.Top();
                    break;

                case(0):
                    start = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
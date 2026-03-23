using   System.Diagnostics;

namespace Sort;
public class Sorter
{
    private List<Record> collection;
    private SortStatistics stats;
    public int id = 1;

    public Sorter()
    {
        InitCollection();
        stats = new SortStatistics();
    }

    public Record CreateRecord()
    {
        Console.WriteLine("Введіть прізвище: ");
        string surname = Console.ReadLine();
        Console.WriteLine("Введіть групу: ");
        string group = Console.ReadLine();
        Console.WriteLine("Введіть середній бал: ");
        double averageScore;

        while (!double.TryParse(Console.ReadLine(), out averageScore) || averageScore < 0 || averageScore > 100)
        {
            Console.WriteLine("Помилка! Введіть коректне число від 0 до 100: ");
        }

        Record record = new Record(id, surname, group, averageScore);
        id++;
        return record;
    }

    public void InitCollection()
    {
        collection = new List<Record>();
    }

    public void AddRecord(Record record)
    {
        collection.Add(record);
    }

    public void RemoveRecord(int studentId)
    {
        for(int i = 0; i < collection.Count; i++)
        {
            if(collection[i].StudentId == studentId)
            {
                collection.RemoveAt(i);
                Console.WriteLine($"Студента з ID {studentId} видалено");
                return;
            }
        }
        Console.WriteLine($"Студента з ID {studentId} не знайдено");
    }

    public void PrintCollection()
    {
        if (collection.Count == 0)
        {
            Console.WriteLine("Колекція порожня");
            return;
        }

        for (int i = 0; i < collection.Count; i++)
        {
            Console.WriteLine(collection[i].ToString());
        }
    }

    public void GenerateControlData()
    {
        InitCollection();

        AddRecord(new Record(1, "Шевченко", "КВ-11", 95.5));
        AddRecord(new Record(2, "Коваленко", "КВ-11", 72.0));
        AddRecord(new Record(3, "Бойко", "КВ-12", 45.0));
        AddRecord(new Record(4, "Ткаченко", "КВ-12", 88.5));
        AddRecord(new Record(5, "Кравченко", "КВ-11", 60.0));
        AddRecord(new Record(6, "Олійник", "КВ-13", 99.0));
        AddRecord(new Record(7, "Поліщук", "КВ-13", 55.5));
        AddRecord(new Record(8, "Марченко", "КВ-11", 75.0));
        AddRecord(new Record(9, "Лисенко", "КВ-12", 82.0));
        AddRecord(new Record(10, "Мельник", "КВ-13", 91.5));

        Console.WriteLine("Колекцію заповнено");
        id = 11;
    }

    public void SortCollection()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        if (collection.Count <= 1) return;

        List<Record>[] buckets = new List<Record>[11];
        int n = buckets.Length;

        for (int i = 0; i < n; i++)
        {
            buckets[i] = new List<Record>();
        }

        for (int i = 0; i < collection.Count; i++)
        {
            Record currentRecord = collection[i];
            
            int bucketIndex = 10 - (int)(currentRecord.AverageScore / 10);
            
            buckets[bucketIndex].Add(currentRecord);
            
            stats.Copies++;
        }

        Console.WriteLine("\n--- Проміжний етап: Розподіл по кошиках ---");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Кошик {i}: {buckets[i].Count} елементів");
        }

        collection.Clear(); 
        
        for (int i = 0; i < n; i++)
        {
            if (buckets[i].Count > 1)
            {
                InsertionSort(buckets[i], buckets[i].Count);
            }

            for (int j = 0; j < buckets[i].Count; j++)
            {
                collection.Add(buckets[i][j]);
                stats.Copies++;
            }
        }

        stopwatch.Stop();
        stats.ExecutionTimeMs = stopwatch.ElapsedMilliseconds;
    }

    public void InsertionSort(List<Record> bucket, int n)
    {
        for (int j = 1; j < n; j++)
        {
            Record key = bucket[j]; 
            int l = j - 1;
            stats.Copies++; 

            while (l >= 0 && bucket[l].AverageScore < key.AverageScore)
            {
                stats.Comparisons++;

                bucket[l + 1] = bucket[l];
                stats.Copies++; 
                        
                l--;
            }
                    
            if(l >= 0)
            {
                stats.Comparisons++;
            }

            bucket[l + 1] = key;
            stats.Copies++; 
        }    
    }

    public void Top()
    {
        if (collection.Count == 0)
        {
            Console.WriteLine("Колекція порожня");
            return;
        }

        Console.WriteLine("\n--- Топ 25% ---");
        
        int top25Count = (int)(collection.Count * 0.25);
        
        Console.WriteLine($"\nТоп 25% студентів (Кількість: {top25Count}):");
        for (int i = 0; i < top25Count; i++)
        {
            Console.WriteLine(collection[i].ToString());
        }

        int count0_59 = 0, count60_73 = 0, count74_89 = 0, count90_100 = 0;

        for (int i = 0; i < collection.Count; i++)
        {
            double score = collection[i].AverageScore;
            
            if (score >= 90) count90_100++;
            else if (score >= 74) count74_89++;
            else if (score >= 60) count60_73++;
            else count0_59++;
        }

        Console.WriteLine("\n--- Розподіл за інтервалами успішності ---");
        Console.WriteLine($"90–100: {count90_100} студентів");
        Console.WriteLine($"74–89: {count74_89} студентів");
        Console.WriteLine($"60–73: {count60_73} студентів");
        Console.WriteLine($"0–59:  {count0_59} студентів");
    }

    public void PrintStatistics()
    {
        Console.WriteLine("\n--- Статистика сортування ---");
        Console.WriteLine($"Кількість порівнянь: {stats.Comparisons}");
        Console.WriteLine($"Кількість копіювань/перестановок: {stats.Copies}");
        Console.WriteLine($"Часу витрачено: {stats.ExecutionTimeMs} ms");
    }

    public void ClearStatistics()
    {
        stats.Reset();
    }
}
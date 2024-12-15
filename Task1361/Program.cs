using System.Diagnostics;

namespace Task1361;

class Program
{
    static void Main(string[] args)
    {
        // Чтение текста из файла
        string path = "Text1.txt";
        string text;

        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            throw new Exception(String.Format("An error ocurred while executing the data import: {0}", e.Message), e);
        }
        
        // Выделяем слова из текста
        var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
        var splitText = noPunctuationText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        
        // Создаём коллекции
        List<string> _list = new List<string>(splitText);
        LinkedList<string> _linkedList = new LinkedList<string>(splitText);
        
        Stopwatch stopwatch = new Stopwatch();
        
        // Сравнение производительности вставки в List<T>
        stopwatch.Start();
        _list.Insert(10, "Hello");
        stopwatch.Stop();
        
        Console.WriteLine($"Время вставки для коллекции List: {stopwatch.Elapsed}");
        
        stopwatch.Reset();
        
        // Сравнение производительности вставки в LinkedList<T> (без поиска ноды для вставки)
        LinkedListNode<string>? node1 = _linkedList.Find(_list[10]);
        
        stopwatch.Start();
        if (node1 != null)
            _linkedList.AddAfter(node1, "Hello");
        stopwatch.Stop();
        
        Console.WriteLine($"Время вставки для коллекции LinkedList (без поиска ноды для вставки): {stopwatch.Elapsed}");
        
        stopwatch.Reset();
        
        // Сравнение производительности вставки в LinkedList<T> (с поиском ноды для вставки, занимает больше времени)
        stopwatch.Start();
        LinkedListNode<string>? node2 = _linkedList.Find("считают");
        if (node2 != null)
            _linkedList.AddAfter(node2, "Hello");
        stopwatch.Stop();
        
        Console.WriteLine($"Время вставки для коллекции LinkedList (с поиском ноды для вставки): {stopwatch.Elapsed}");
        
        stopwatch.Reset();
    }
}
namespace Task1362;

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
        
        
        // Определяем частоту слов в тексте
        var wordsChart = new Dictionary<string, int>();

        foreach (var word in splitText)
        {
            if (wordsChart.ContainsKey(word.ToLower()))
            {
                wordsChart[word.ToLower()]++;
            }
            else
            {
                wordsChart.Add(word.ToLower(), 1);
            }
        }
        
        // Выделяем 10 частых слов из текста
        var topTenWords = wordsChart.OrderByDescending(w => w.Value).Take(10).ToDictionary();

        // Вывод результата
        foreach (var word in topTenWords)
        {
            Console.WriteLine($"Слово \"{word.Key}\" встречается {word.Value} раз");
        }
    }
}
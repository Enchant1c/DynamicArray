using System;

class Program
{
    static void Main()
    {
        var array = new DynamicArray<int>();

        
        array.Add(1);
        array.Add(2);
        array.Add(3);
        PrintArray(array, "После добавления элемента: ");
        
        array.AddRange(new int[] {9, 9, 11});
        PrintArray(array, "После добавления элементов: ");

        Console.WriteLine($"Проверка: {array.Any()}");
        Console.WriteLine("22");
        Console.WriteLine($"Первый элемент: {array.First()}");
        
        array.Insert(7, 2);
        PrintArray(array, "После вставки: ");

        Console.WriteLine($"Индекс первого вхождения: {array.IndexOf(9)}");

        Console.WriteLine($"Индекс последнего вхождения: {array.LastIndexOf(9)}");
        
        bool removed = array.Remove(9);
        PrintArray(array, "После удаления: ");
        Console.WriteLine($"Элемент удален?: {removed}");
        
        int removedCount = array.RemoveAll(new int[] { 1, 3 });
        PrintArray(array, $"Удалено {removedCount} элементов, После удаления:");
        
        array.Clear();
        PrintArray(array, "После очистки коллекции: ");
    }
    /// <summary>
    /// Вывод массива
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="message"></param>
        private static void PrintArray<T>(DynamicArray<T> array, string message)
        {
            Console.WriteLine($"{message}[{string.Join(", ", array.ToArray())}]");
        }
}
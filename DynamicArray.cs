using System;

public class DynamicArray<T>
{
    private T[] elements;
    private int count;
    private int capacity; 
    
    /// <summary>
    /// Конструктор коллекции
    /// </summary>
    public DynamicArray()
    {
        capacity = 3;
        elements = new T[capacity];
        count = 0;
    }
    /// <summary>
    /// Возвращает текущую емкость массива:
    /// </summary>
    public int Capacity => capacity;
    /// <summary>
    /// Возвращает текущее количество элементов:
    /// </summary>
    public int Count => count;
    /// <summary>
    /// Добавление элемента
    /// </summary>
    /// <param name="item"></param>    
    public void Add(T item)
    {
        if (count == capacity)
            ResizeArray(capacity + 1);
        elements[count++] = item;
    }
    /// <summary>
    /// Добавление нескольких элементов
    /// </summary>
    /// <param name="items"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddRange(T[] items)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        int required = count + items.Length;
        if (required > capacity)
        {            
            ResizeArray(required);
        }
        Array.Copy(items, 0, elements, count, items.Length);
        count += items.Length;
    }
    /// <summary>
    /// Проверка на элементы в коллекции
    /// </summary>
    /// <returns></returns>
    public bool Any() => count > 0;
    /// <summary>
    /// Возвращает первый элемент
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T First()
    {
        if (count == 0)
            throw new InvalidOperationException("Коллекция пуста");
        return elements[0];
    }
    /// <summary>
    /// Удаляет первый найденый элемент
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index == -1)
            return false;
        RemoveAt(index);
        return true;
    }
    /// <summary>
    /// Удаляет все вхождения элемента из колекции
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public int RemoveAll(T[] items)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        int originalCount = count;
        int newCount = 0;

        for (int i = 0; i < count; i++)
        {
            bool found = false;
            foreach (var item in items)
            {
                if (elements[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                elements[newCount++] = elements[i];
        }

        count = newCount;
        return originalCount - newCount;
    }
    /// <summary>
    /// Очищает коллекцию
    /// </summary>
    public void Clear() => count = 0;
    /// <summary>
    /// Добавляет элемент по индексу
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void Insert(T item, int index)
    {
        if (index < 0 || index > count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (count == capacity)
            ResizeArray(capacity + 1);

        for (int i = count; i > index; i--)
            elements[i] = elements[i - 1];

        elements[index] = item;
        count++;
    }
    /// <summary>
    /// Возвращает индекс первого вхождения элемента в коллекции
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int IndexOf(T item)
    {
        for (int i = 0; i < count; i++)
            if (elements[i].Equals(item))
                return i;
        return -1;
    }
    /// <summary>
    /// Возвращает индекс последнего вхождения элемента в коллекции
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int LastIndexOf(T item)
    {
        for (int i = count - 1; i >= 0; i--)
            if (elements[i].Equals(item))
                return i;
        return -1;
    }
    /// <summary>
    /// Изменяем емкость коллекции
    /// </summary>
    /// <param name="newCapacity"></param>
    private void ResizeArray(int newCapacity)
    {
        T[] newElements = new T[newCapacity];
        Array.Copy(elements, newElements, count);
        elements = newElements;
        capacity = newCapacity;
    }
    /// <summary>
    /// Удаляет элемент по указанному индексу.
    /// </summary>
    /// <param name="index"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException(nameof(index));

        for (int i = index; i < count - 1; i++)
            elements[i] = elements[i + 1];
        count--;
    }
    /// <summary>
    /// Вывод коллекции
    /// </summary>
    /// <returns></returns>
    public T[] ToArray()
    {
        T[] result = new T[count];
        Array.Copy(elements, result, count);
        return result;
    }
    
}

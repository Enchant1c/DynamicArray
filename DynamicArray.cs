using System;

public class DynamicArray<T>
{
    /// <summary>
    /// Элементы коллекции
    /// </summary>
    private T[] _elements;
    /// <summary>
    /// Текущее кол-во элементов в коллекции
    /// </summary>
    private int _count;
    /// <summary>
    /// Емкость коллекции
    /// </summary>
    private int _capacity; 
    
    /// <summary>
    /// Конструктор коллекции
    /// </summary>
    public DynamicArray()
    {
        _capacity = 3;
        _elements = new T[_capacity];
        _count = 0;
    }
    /// <summary>
    /// Возвращает текущую емкость массива:
    /// </summary>
    public int Capacity => _capacity;
    /// <summary>
    /// Возвращает текущее количество элементов:
    /// </summary>
    public int Count => _count;
    /// <summary>
    /// Добавление элемента
    /// </summary>
    /// <param name="item"></param>    
    public void Add(T item)
    {
        if (_count == _capacity)
            ResizeArray(_capacity + 1);
        _elements[_count++] = item;
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

        int required = _count + items.Length;
        if (required > _capacity)
        {            
            ResizeArray(required);
        }
        Array.Copy(items, 0, _elements, _count, items.Length);
        _count += items.Length;
    }
    /// <summary>
    /// Проверка на элементы в коллекции
    /// </summary>
    /// <returns></returns>
    public bool Any() => _count > 0;
    /// <summary>
    /// Возвращает первый элемент
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T First()
    {
        if (_count == 0)
            throw new InvalidOperationException("Коллекция пуста");
        return _elements[0];
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

        int originalCount = _count;
        int newCount = 0;

        for (int i = 0; i < _count; i++)
        {
            bool found = false;
            foreach (var item in items)
            {
                if (_elements[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                _elements[newCount++] = _elements[i];
        }

        _count = newCount;
        return originalCount - newCount;
    }
    /// <summary>
    /// Очищает коллекцию
    /// </summary>
    public void Clear() => _count = 0;
    /// <summary>
    /// Добавляет элемент по индексу
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void Insert(T item, int index)
    {
        if (index < 0 || index > _count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (_count == _capacity)
            ResizeArray(_capacity + 1);

        for (int i = _count; i > index; i--)
            _elements[i] = _elements[i - 1];

        _elements[index] = item;
        _count++;
    }
    /// <summary>
    /// Возвращает индекс первого вхождения элемента в коллекции
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int IndexOf(T item)
    {
        for (int i = 0; i < _count; i++)
            if (_elements[i].Equals(item))
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
        for (int i = _count - 1; i >= 0; i--)
            if (_elements[i].Equals(item))
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
        Array.Copy(_elements, newElements, _count);
        _elements = newElements;
        _capacity = newCapacity;
    }
    /// <summary>
    /// Удаляет элемент по указанному индексу.
    /// </summary>
    /// <param name="index"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
            throw new ArgumentOutOfRangeException(nameof(index));

        for (int i = index; i < _count - 1; i++)
            _elements[i] = _elements[i + 1];
        _count--;
    }
    /// <summary>
    /// Вывод коллекции
    /// </summary>
    /// <returns></returns>
    public T[] ToArray()
    {
        T[] result = new T[_count];
        Array.Copy(_elements, result, _count);
        return result;
    }
    
}

// Maximilianno jose de leon cera
// programacion
// Grupo: 30


using System;

using System.Collections.Generic;
using System.IO;

// Excepción personalizada
public class InvalidNumberException : Exception
{
    public InvalidNumberException(string message) : base(message) { }
}

// Clase que maneja los métodos de ordenamiento
public class NumberSorter
{
    public List<int> Numbers { get; private set; } = new List<int>();

    public void AddNumber(int number)
    {
        if (Numbers.Contains(number))
        {
            throw new InvalidNumberException("The number is a duplicate.");
        }
        Numbers.Add(number);
    }

    public void Sort(int choice)
    {
        switch (choice)
        {
            case 1: BubbleSort(); break;
            case 2: ShellSort(); break;
            case 3: SelectionSort(); break;
            case 4: InsertionSort(); break;
            default: throw new ArgumentException("Invalid sorting method choice.");
        }
    }

    private void BubbleSort()
    {
        for (int i = 0; i < Numbers.Count - 1; i++)
            for (int j = 0; j < Numbers.Count - 1 - i; j++)
                if (Numbers[j] > Numbers[j + 1])
                    (Numbers[j], Numbers[j + 1]) = (Numbers[j + 1], Numbers[j]);
    }

    private void ShellSort()
    {
        for (int gap = Numbers.Count / 2; gap > 0; gap /= 2)
            for (int i = gap; i < Numbers.Count; i++)
            {
                int temp = Numbers[i], j;
                for (j = i; j >= gap && Numbers[j - gap] > temp; j -= gap)
                    Numbers[j] = Numbers[j - gap];
                Numbers[j] = temp;
            }
    }

    private void SelectionSort()
    {
        for (int i = 0; i < Numbers.Count - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < Numbers.Count; j++)
                if (Numbers[j] < Numbers[minIndex])
                    minIndex = j;
            (Numbers[i], Numbers[minIndex]) = (Numbers[minIndex], Numbers[i]);
        }
    }

    private void InsertionSort()
    {
        for (int i = 1; i < Numbers.Count; i++)
        {
            int key = Numbers[i], j = i - 1;
            while (j >= 0 && Numbers[j] > key)
                Numbers[j + 1] = Numbers[j--];
            Numbers[j + 1] = key;
        }
    }
}

class Program
{
    static void Main()
    {
        var sorter = new NumberSorter();
        Console.WriteLine("Please enter 10 different numbers:");

        while (sorter.Numbers.Count < 10)
        {
            Console.Write($"Enter number {sorter.Numbers.Count + 1}: ");
            try
            {
                int inputNumber = int.Parse(Console.ReadLine() ?? throw new InvalidNumberException("Input is not a valid number."));
                sorter.AddNumber(inputNumber);
            }
            catch (InvalidNumberException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        Console.WriteLine("Choose a sorting method:\n1. Bubble Sort\n2. Shell Sort\n3. Selection Sort\n4. Insertion Sort");
        try
        {
            int choice = int.Parse(Console.ReadLine() ?? throw new ArgumentException("Invalid choice."));
            sorter.Sort(choice);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Sorted numbers: " + string.Join(", ", sorter.Numbers));
        File.WriteAllLines("SortedNumbers.txt", sorter.Numbers.ConvertAll(n => n.ToString()));
    }
}

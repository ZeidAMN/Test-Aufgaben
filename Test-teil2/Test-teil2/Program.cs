using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

class IntegerListManager
{
    private List<int> numbers;
    private static Mutex mutex = new Mutex(); // Mutex für die Datei-Synchronisation
    private static string directoryPath = @"C:\Users\FP2402199\Downloads\Test-teil2"; // Basisverzeichnis
    private static string fileName = "output.txt"; // Festgelegter Dateiname

    public IntegerListManager()
    {
        numbers = new List<int>();
    }

    public void AddNumber(int number)
    {
        numbers.Add(number);
    }

    public void RemoveNumber(int number)
    {
        if (numbers.Contains(number))
        {
            numbers.Remove(number);
            Console.WriteLine($"Zahl {number} wurde entfernt.");
        }
        else
        {
            Console.WriteLine($"Zahl {number} nicht in der Liste gefunden.");
        }
    }

    public double? CalculateAverage()
    {
        if (numbers.Count > 0)
        {
            return numbers.Average();
        }
        else
        {
            return null;
        }
    }

    public double? CalculateMedian()
    {
        if (numbers.Count > 0)
        {
            var sortedNumbers = numbers.OrderBy(n => n).ToList();
            int count = sortedNumbers.Count;
            if (count % 2 == 0)
            {
                return (sortedNumbers[count / 2 - 1] + sortedNumbers[count / 2]) / 2.0;
            }
            else
            {
                return sortedNumbers[count / 2];
            }
        }
        else
        {
            return null;
        }
    }

    public void SaveToFile()
    {
        string filePath = Path.Combine(directoryPath, fileName);

        try
        {
            mutex.WaitOne(); // Mutex erwerben, um exklusiven Zugriff zu gewährleisten

            // Schreiben der Liste in die Datei
            using (StreamWriter writer = new StreamWriter(filePath, false)) // false für Überschreiben der Datei
            {
                foreach (var number in numbers)
                {
                    writer.WriteLine(number);
                }
            }

            Console.WriteLine($"Liste wurde in {fileName} gespeichert.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Schreiben in die Datei: " + ex.Message);
        }
        finally
        {
            mutex.ReleaseMutex(); // Mutex freigeben
        }
    }

    public void LoadFromFile()
    {
        string filePath = Path.Combine(directoryPath, fileName);

        if (File.Exists(filePath))
        {
            numbers = new List<int>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (int.TryParse(line, out int number))
                    {
                        numbers.Add(number);
                    }
                }
            }
            Console.WriteLine($"Liste aus {fileName} geladen.");
        }
        else
        {
            Console.WriteLine($"Datei {fileName} nicht gefunden.");
        }
    }

    public void PrintNumbers()
    {
        if (numbers.Count > 0)
        {
            Console.WriteLine("Liste: " + string.Join(", ", numbers));
        }
        else
        {
            Console.WriteLine("Die Liste ist leer.");
        }
    }
}

class Program
{
    private static void Main(string[] args)
    {
        IntegerListManager manager = new IntegerListManager();
        int wahl;

        do
        {
            Console.Clear();
            Console.WriteLine("Integer Listen Manager:");
            Console.WriteLine("1. Zahl hinzufügen");
            Console.WriteLine("2. Alle Zahlen anzeigen");
            Console.WriteLine("3. Zahl entfernen");
            Console.WriteLine("4. Durchschnitt berechnen");
            Console.WriteLine("5. Median berechnen");
            Console.WriteLine("6. Liste speichern");
            Console.WriteLine("7. Liste laden");
            Console.WriteLine("8. Beenden");
            Console.Write("Wähle eine Option: ");
            if (!int.TryParse(Console.ReadLine(), out wahl))
            {
                wahl = 0;
            }

            Console.Clear();

            switch (wahl)
            {
                case 1:
                    Console.Write("Geben Sie eine Zahl ein, die hinzugefügt werden soll: ");
                    if (int.TryParse(Console.ReadLine(), out int numberToAdd))
                    {
                        manager.AddNumber(numberToAdd);
                        Console.WriteLine($"Zahl {numberToAdd} hinzugefügt.");
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Zahl.");
                    }
                    break;

                case 2:
                    manager.PrintNumbers();
                    break;

                case 3:
                    manager.PrintNumbers(); // Liste anzeigen, bevor eine Zahl entfernt wird
                    Console.Write("Geben Sie eine Zahl ein, die entfernt werden soll: ");
                    if (int.TryParse(Console.ReadLine(), out int numberToRemove))
                    {
                        manager.RemoveNumber(numberToRemove);
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Zahl.");
                    }
                    break;

                case 4:
                    double? average = manager.CalculateAverage();
                    Console.WriteLine("Durchschnitt: " + (average.HasValue ? average.ToString() : "N/A"));
                    break;

                case 5:
                    double? median = manager.CalculateMedian();
                    Console.WriteLine("Median: " + (median.HasValue ? median.ToString() : "N/A"));
                    break;

                case 6:
                    manager.SaveToFile();
                    break;

                case 7:
                    manager.LoadFromFile();
                    break;

                case 8:
                    Console.WriteLine("Programm wird beendet...");
                    break;

                default:
                    Console.WriteLine("Ungültige Wahl. Bitte eine Zahl zwischen 1 und 8 eingeben.");
                    break;
            }

            if (wahl != 8)
            {
                Console.WriteLine("\nDrücken Sie eine beliebige Taste, um fortzufahren...");
                Console.ReadKey();
            }

        } while (wahl != 8);
    }
}

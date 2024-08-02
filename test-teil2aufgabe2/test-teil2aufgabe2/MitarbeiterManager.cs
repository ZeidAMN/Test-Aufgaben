using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_teil2aufgabe2
{
    public class MitarbeiterManager
    {
        private List<Mitarbeiter> mitarbeiterListe = new List<Mitarbeiter>();
        private static string filePath = @"C:\Users\FP2402199\Downloads\Test-teil2\mitarbeiterDaten.txt"; // Pfad zur Datei

        public void AddMitarbeiter(Mitarbeiter mitarbeiter)
        {
            mitarbeiterListe.Add(mitarbeiter);
        }

        public void PrintMitarbeiter()
        {
            if (mitarbeiterListe.Count > 0)
            {
                foreach (var mitarbeiter in mitarbeiterListe)
                {
                    Console.WriteLine(mitarbeiter);
                }
            }
            else
            {
                Console.WriteLine("Keine Mitarbeiterdaten vorhanden.");
            }
        }

        public Mitarbeiter SearchMitarbeiter(string vorname, string nachname)
        {
            return mitarbeiterListe.FirstOrDefault(m => m.Vorname == vorname && m.Nachname == nachname);
        }

        public double MaxGehalt()
        {
            return mitarbeiterListe.Max(m => m.Gehalt);
        }

        public double MinGehalt()
        {
            return mitarbeiterListe.Min(m => m.Gehalt);
        }

        public double SummeGehalt()
        {
            return mitarbeiterListe.Sum(m => m.Gehalt);
        }

        public double StandardabweichungGehalt()
        {
            double mean = mitarbeiterListe.Average(m => m.Gehalt);
            double variance = mitarbeiterListe.Average(m => Math.Pow(m.Gehalt - mean, 2));
            return Math.Sqrt(variance);
        }

        public double MedianGehalt()
        {
            var sortedGehalt = mitarbeiterListe.Select(m => m.Gehalt).OrderBy(g => g).ToList();
            int count = sortedGehalt.Count;
            if (count % 2 == 0)
            {
                return (sortedGehalt[count / 2 - 1] + sortedGehalt[count / 2]) / 2.0;
            }
            else
            {
                return sortedGehalt[count / 2];
            }
        }

        public void SaveToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Mitarbeiter Daten:");
                    foreach (var mitarbeiter in mitarbeiterListe)
                    {
                        writer.WriteLine(mitarbeiter);
                    }

                    writer.WriteLine();
                    writer.WriteLine($"Maximalgehalt: {MaxGehalt()}");
                    writer.WriteLine($"Minimalgehalt: {MinGehalt()}");
                    writer.WriteLine($"Summe der Gehälter: {SummeGehalt()}");
                    writer.WriteLine($"Standardabweichung der Gehälter: {StandardabweichungGehalt()}");
                    writer.WriteLine($"Median der Gehälter: {MedianGehalt()}");
                }
                Console.WriteLine($"Daten wurden in die Datei '{filePath}' gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Speichern der Daten in die Datei: " + ex.Message);
            }
        }
    }
}

using System;

namespace test_teil2aufgabe2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MitarbeiterManager manager = new MitarbeiterManager();

            // Beispiel-Daten hinzufügen
            manager.AddMitarbeiter(new Mitarbeiter("Max", "Mustermann", "Vollzeit", 3000, 5));
            manager.AddMitarbeiter(new Mitarbeiter("Anna", "Musterfrau", "Teilzeit", 2500, 3));
            manager.AddMitarbeiter(new Mitarbeiter("Peter", "Pan", "Vollzeit", 4000, 10));

            int wahl;

            do
            {
                Console.Clear();
                Console.WriteLine("Mitarbeiter Manager:");
                Console.WriteLine("1. Mitarbeiter hinzufügen");
                Console.WriteLine("2. Alle Mitarbeiter anzeigen");
                Console.WriteLine("3. Mitarbeiter suchen");
                Console.WriteLine("4. Daten speichern");
                Console.WriteLine("5. Beenden");
                Console.Write("Wähle eine Option: ");
                if (!int.TryParse(Console.ReadLine(), out wahl))
                {
                    wahl = 0;
                }

                Console.Clear();

                switch (wahl)
                {
                    case 1:
                        Console.Write("Geben Sie den Vornamen ein: ");
                        string vorname = Console.ReadLine();
                        Console.Write("Geben Sie den Nachnamen ein: ");
                        string nachname = Console.ReadLine();
                        Console.Write("Geben Sie den Status im Betrieb ein: ");
                        string statusImBetrieb = Console.ReadLine();
                        Console.Write("Geben Sie das Gehalt ein: ");
                        if (double.TryParse(Console.ReadLine(), out double gehalt))
                        {
                            Console.Write("Geben Sie die Anzahl der Jahre im Betrieb ein: ");
                            if (int.TryParse(Console.ReadLine(), out int jahre))
                            {
                                manager.AddMitarbeiter(new Mitarbeiter(vorname, nachname, statusImBetrieb, gehalt, jahre));
                                Console.WriteLine("Mitarbeiter wurde hinzugefügt.");
                            }
                            else
                            {
                                Console.WriteLine("Ungültige Anzahl der Jahre.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ungültiges Gehalt.");
                        }
                        break;

                    case 2:
                        manager.PrintMitarbeiter();
                        break;

                    case 3:
                        Console.Write("Geben Sie den Vornamen des Mitarbeiters ein, den Sie suchen möchten: ");
                        string suchVorname = Console.ReadLine();
                        Console.Write("Geben Sie den Nachnamen des Mitarbeiters ein: ");
                        string suchNachname = Console.ReadLine();
                        Mitarbeiter gefunden = manager.SearchMitarbeiter(suchVorname, suchNachname);
                        if (gefunden != null)
                        {
                            Console.WriteLine("Gefundener Mitarbeiter: " + gefunden);
                        }
                        else
                        {
                            Console.WriteLine("Mitarbeiter nicht gefunden.");
                        }
                        break;

                    case 4:
                        manager.SaveToFile();
                        break;

                    case 5:
                        Console.WriteLine("Programm wird beendet...");
                        break;

                    default:
                        Console.WriteLine("Ungültige Wahl. Bitte eine Zahl zwischen 1 und 5 eingeben.");
                        break;
                }

                if (wahl != 5)
                {
                    Console.WriteLine("\nDrücken Sie eine beliebige Taste, um fortzufahren...");
                    Console.ReadKey();
                }

            } while (wahl != 5);
        }
    }
    
}

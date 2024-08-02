using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_teil2aufgabe2
{
    public class Mitarbeiter
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string StatusImBetrieb { get; set; }
        public double Gehalt { get; set; }
        public int AnzahlJahreImBetrieb { get; set; }

        public Mitarbeiter(string _vorname, string _nachname, string _statusImBetrieb, double _gehalt, int _anzahlJahreImBetrieb)
        {
            Vorname = _vorname;
            Nachname = _nachname;
            StatusImBetrieb = _statusImBetrieb;
            Gehalt = _gehalt;
            AnzahlJahreImBetrieb = _anzahlJahreImBetrieb;
        }

        public override string ToString()
        {
            return $"Vorname: {Vorname}, Nachname: {Nachname}, Status: {StatusImBetrieb}, Gehalt: {Gehalt}, Jahre im Betrieb: {AnzahlJahreImBetrieb}";
        }
    }
}

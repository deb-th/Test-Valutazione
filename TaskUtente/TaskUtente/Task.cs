using System;
using System.Collections.Generic;
using System.Text;

namespace TaskUtente
{
    public class Task
    {
        //Definizione delle variabile che caratterizzano un task e
        //Definizione delle relative proprietà per accedervi

        public string Descrizione { get; set; }

        public DateTime DataScadenza { get; set; }

        //enum Livello
        //{
        //    Basso,
        //    Medio,
        //    Alto
        //}
            
        public string LivelloImportanza { get; set; } //qui mi dava problemi se utilizzano enum

        //metodo per fornire i dati completi del task
        public string GetTask()
        {
            return Descrizione + " " + DataScadenza + " " + LivelloImportanza;
        }
    }
}

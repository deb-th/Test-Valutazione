using System;

namespace TaskUtente
{
    class Program
    {
        static void Main(string[] args)
        {
            char key;

            do
            {
                //Scelta dell'operazione
                TaskManagement.SceltaOperazione();

                Console.WriteLine("Se vuoi uscire dal programma premi 5, altrimenti scegli l'operazione da eseguire");
                key = Console.ReadKey().KeyChar;

            } while (key != 5); //L'utente ha deciso di uscire dal programma
        }
    }
}
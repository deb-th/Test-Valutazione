using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TaskUtente
{
    public class TaskManagement
    {
        public static string path { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test Valutazione/Task.csv");

        //Metodo per leggere i task da file csv e inserirli in un array
        public static Task[] GetAllTask()
        {
            int totLines = File.ReadLines(path).Count(); //Linee totali nel file
            Task[] tasks = new Task[totLines - 1]; //Array dei task senza intestazione

            string line; //riga task

            using (StreamReader reader = File.OpenText(path))
            {
                //riga 0 intestazione
                string header = reader.ReadLine();

                for (int i = 0; i < totLines - 1; i++)
                {
                    line = reader.ReadLine();
                    string[] taskInfo = line.Split(",");

                    Task task = new Task
                    {
                        Descrizione = taskInfo[0],
                        DataScadenza = Convert.ToDateTime(taskInfo[1]),
                        LivelloImportanza = taskInfo[2],
                    };
                    tasks[i] = task;
                }
            }
            return tasks;
        }

        //Metodo per la scelta delle operazioni da eseguire
        public static void SceltaOperazione()
        {
            Console.WriteLine("Scegliere l'operazione da eseguire!");
            Console.WriteLine("Premere 1 per Visualizzare i task inseriti");
            Console.WriteLine("Premere 2 per Aggiungere un nuovo task");
            Console.WriteLine("Premere 3 per Eliminare un task");
            Console.WriteLine("Premere 4 per Filtrare i task per importanza");
            Console.WriteLine("Premere 5 per Uscire");

            Task[] task = GetAllTask();
            Task newTask = new Task();
            char key = (char)Console.ReadKey().KeyChar;
            Console.Write("\n");

            switch (key)
            {
                case '1':
                    ViewTasks(task);
                    break;
                case '2':
                    Console.WriteLine("Inserire la descrizione del task da aggiungere!");
                    string descrizione = Console.ReadLine();
                    Console.WriteLine("Inserire la data di scadenza del task!");
                    DateTime data = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Inserire il livello di importanza del task! (Basso, Medio, Alto)");
                    string importanza = Console.ReadLine();
                    newTask.Descrizione = descrizione;
                    newTask.DataScadenza = data;
                    newTask.LivelloImportanza = importanza;
                    AddTask(newTask);
                    break;
                case '3':
                    Console.WriteLine("Inserire la descrizione del task da eliminare!");
                    string descr = Console.ReadLine();
                    newTask.Descrizione = descr;
                    DeleteTask(newTask);
                    break;
                case '4':
                    Console.WriteLine("Inserire il livello di importanza dei task da visualizzare (Basso, Medio, Alto)!");
                    string liv = Console.ReadLine();
                    newTask.Descrizione = liv;
                    GetTaskByImportanceLevel(liv);
                    break;
                case '5':
                    break;
                default:
                    Console.WriteLine("Funzione non è disponibile! Scegliere un comando valido!");
                    break;
            }
        }

        //Metodo che mostra i task inseriti
        public static void ViewTasks(Task[] tasks)
        {
            Console.WriteLine("I task inseriti dall'utente sono:");
            foreach (Task task in tasks)
            {
                Console.WriteLine(task.GetTask());
            }
        }

        //Metodo per inserire un nuovo task fornito dall'utente
        public static bool AddTask(Task newTask)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(newTask.Descrizione + "," + newTask.DataScadenza + "," + newTask.LivelloImportanza);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Metodo per eliminare un task fornito dall'utente
        public static void DeleteTask(Task task)
        {
            Task[] tasks = GetAllTask();
            ArrayList upTask = new ArrayList();

            foreach (Task t in tasks)
            {
                if ((t.Descrizione != task.Descrizione))
                {
                    upTask.Add(t);
                }
                else
                {
                    using (StreamWriter writer = File.CreateText(path))
                    {
                        writer.WriteLine("FirstName,LastName,Role,Department");
                        foreach (Task ta in upTask)
                        {
                            writer.WriteLine(ta.Descrizione+ "," + ta.DataScadenza + "," + ta.LivelloImportanza);
                        }
                    }
                }
            }
        }

        //Metodo per filtrare i task in base al livello di importanza selezionato dall'utente
        public static Task[] GetTaskByImportanceLevel(string livello)
        {
            Task[] allTask = GetAllTask();
            ArrayList filteredTasks = new ArrayList();

            for (int i = 0; i < allTask.Length; i++)
            {
                if (allTask[i].LivelloImportanza == livello)
                {
                    filteredTasks.Add(allTask[i]);
                }
            }
            return (Task[])filteredTasks.ToArray(typeof(Task));
        }
    }
}

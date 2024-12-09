﻿using Microsoft.EntityFrameworkCore;

namespace jrnl
{
    public class ListCommand : Command
    {

        public override string Name => "list";

        public override void Execute(string[] args)
        {
            var task = GetListAsync();
            List<JournalEntry> journal = task.Result;
            PrintList(journal);
        }

        private async Task<List<JournalEntry>> GetListAsync () 
        {
            List<JournalEntry> list = new();

            try 
            {
                using var db = new JournalContext();
                list = await db.JournalEntries.ToListAsync();
            }
            catch (Exception e)
            {
                PrintFailure(e);
            }

            return list;
        }

        private void PrintList (List<JournalEntry> list) 
        {
            int count = list.Count();

            Console.WriteLine("Your journal has ({0}) {1}{2}", 
                                count, 
                                count == 1 ? "entry" : "entries",
                                count > 0 ? ":" : ".");

            foreach (JournalEntry journalEntry in list)
            {
                Console.WriteLine("{0,3} | {1,-16} | {2,-10} | {3,-36}",
                                    journalEntry.Id,
                                    Truncate(journalEntry.Title, 16),
                                    journalEntry.Date.ToString("d"),
                                    Truncate(journalEntry.Body, 36));
            }
        }

        private void PrintFailure (Exception e) 
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException?.Message);
        }

        private static string Truncate (string? original, int maxLength) 
        {
            if (String.IsNullOrEmpty(original)) return "";
            if (original.Length <= maxLength) return original;

            if (maxLength > 3) 
                return original.Substring(0, maxLength-3) + "...";
            else 
                return original.Substring(0, maxLength);
        }

    }
}

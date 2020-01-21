using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncTest
{
    class AsyncTest
    {
        static void Main(string[] args)
        {
            AsyncTest obj = new AsyncTest();

            // Part 1 : Introduction 
            // Depicting Task.Run().
            // Sync and async ways of reading and writing to a file.
            obj.Task1(10);

            // Part 2 : Obtaining the results from an asynchronous operation
            obj.Task2(10);
        }
        
        // async in the function definition indicates that there is an asynchronous op in the function
        // it needs to be used in combination with the await keyword
        private async void Task1(int index)
        {
            // Sync Method
            var lines = File.ReadAllLines(@"C:\Users\sasistla\Documents\Pluralsight\AsyncProgramming\test.csv");
            var line = lines.ElementAt(index);
            File.WriteAllText(@"C:\Users\sasistla\Documents\Pluralsight\AsyncProgramming\outputFile.txt", "Read Synchronously :" + line);

            // Async Way to read the File
            // The entire code block within the Task.Run will be executed within a different thread

            // await is a way of letting the code know that once the block specified with await has completed its execution is async and must be waited on.
            await Task.Run(() =>
            {
                var linesAsync = File.ReadAllLines(@"C:\Users\sasistla\Documents\Pluralsight\AsyncProgramming\test.csv");
                var lineAsync = linesAsync.ElementAt(index);

                File.WriteAllText(@"C:\Users\sasistla\Documents\Pluralsight\AsyncProgramming\outputFileAsync.txt", "Read Asynchronously : " + lineAsync);
            });
        }

        // async op which returns a value
        private void Task2(int index)
        {
            // say we have two large ops
            // need to perform one after the other
            // we can do this using continuation

            // the output from the task object is stored in variable
            var task = Task.Run(() =>
            {
                var lines = File.ReadAllLines(@"C:\Users\sasistla\Documents\Pluralsight\AsyncProgramming\test.csv");
                return lines;
            });

            // this represents the continuation block
            // will be executed after task has been executed

            // task.ContinueWith is also executed aynchronously
            // This also returns a task
            // we can chain this also with a continue with block
            task.ContinueWith(t =>
            {
                var linesRead = t.Result;
                var lineAtIndex = linesRead.ElementAt(10);

                File.WriteAllText(@"C:\Users\sasistla\Documents\Pluralsight\AsyncProgramming\fileOutputTask2.txt", "Read Async and Written in Continuation Block\n"+lineAtIndex);
            });
        }
    }
}

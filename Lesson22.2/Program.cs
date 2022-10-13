using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson22._2
{
    class Program
    {
        static object _lock = new object();
        static void Main(string[] args)
        {
            //WriteAndReadFileAsync();
            WriteAndReadFileAsync2();
            WriteAndReadFileAsync3();
            Console.ReadLine();
        }

        static async void WriteAndReadFileAsync2()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.Write(i + " ");
                    Thread.Sleep(100);
                }
            });
        }

                static async void WriteAndReadFileAsync3()
        {
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    for (int i = 99; i != 0; i--)
                    {
                        Console.Write(i + " ");
                        Thread.Sleep(100);
                    }
                }
            }); 
        }

        static async void WriteAndReadFileAsync()
        {
            var _str = await Task.Run(() =>
            {
               Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                return WriteFile("File1.xtx");
            });
            await Task.Run(() => _ReaderFile(_str));
            {
                _ReaderFile(_str);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            }
        }

        private async static Task<string> WriteFile(string _fileName)
        {
            _fileName = "d://" + _fileName;

            using (var _file = File.Create(_fileName))
            {
                using (var _writer = new StreamWriter(_file))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await _writer.WriteLineAsync("This is Async");
                        Thread.Sleep(500);
                    }
                }
            }

            return _fileName;
        }

        private static void _ReaderFile(string _fileName)
        {
            var _file = File.OpenRead(_fileName);
            var _reader = new StreamReader(_file);
            Console.WriteLine(_reader.ReadToEnd());

            _reader.Close();
            _file.Close();
        }
    }
}

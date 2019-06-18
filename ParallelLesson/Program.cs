using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(
                () =>
                {
                    Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) - 1");
                },
                ()=> 
                {
                    Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) - 2");
                },
                ()=> 
                {
                    Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) - 3");
                });

            Console.WriteLine("Стандартное:");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine("\n\nПараалельно:");
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) {i}");
            });

            Parallel.ForEach(new int[] { 1, 2, 3, 4, 5 }, number =>
                 {

                 });

            var users = new List<User>
            {
                new User
                {
                    Login = "Vasya",
                    Password = "vbhfvbhjjh"
                },
                new User
                {
                    Login = "Petya",
                    Password = "vbdfbmjgm"
                },
                new User
                {
                    Login = "Masha",
                    Password = "ngfntynyng"
                },
                new User
                {
                    Login = "Sasha",
                    Password = "jumgngfnfngfn"
                },
                new User
                {
                    Login = "Mitya",
                    Password = "mmjmgfgntnn"
                },
                new User
                {
                    Login = "Jino",
                    Password = "wegtrtynn"
                },
                new User
                {
                    Login = "Misha",
                    Password = "itjhiyijffgn"
                },
                new User
                {
                    Login = "Lena",
                    Password = "khgjnjgbfg"
                }
            };

            List<object> someResult = new List<object>();
            foreach (var user in users)
            {
                if (user.Login.ToLower().Contains("ad"))
                {
                    someResult.Add(new { Name = user.Login });
                }
            }

            // обычным способом последовательно
            //var result = from user in users
            //             where user.Login.ToLower().Contains("ad")
            //             select new { Name = user.Login };

            //var shortResult = users
            //    .Where(user => user.Login.ToLower().Contains("ad"))
            //    .Select(user => new { Name = user.Login });

            // более быстро но не последовательно
            var result = from user in users.AsParallel()
                         where user.Login.ToLower().Contains("ad")
                         select new { Name = user.Login };

            var shortResult = users.AsParallel()
                .Where(user => user.Login.ToLower().Contains("ad"))
                .Select(user => new { Name = user.Login });

            Console.ReadLine();
        }
    }
}

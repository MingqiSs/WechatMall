using StackExchange.Redis;
using System;

namespace QuoteConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.70.236:6379"))
            {
                ISubscriber sub = redis.GetSubscriber();

                Console.WriteLine("请输入任意字符，输入exit退出");

                string input;

                do
                {
                     Random rd = new Random();
                    input = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}_{rd.Next(10, 100).ToString()}";// Console.ReadLine();
                  //  input = Console.ReadLine();               
                    sub.Publish("QuoteMessages1", input);
                } while (input != "exit");
            }
        }
    }
}

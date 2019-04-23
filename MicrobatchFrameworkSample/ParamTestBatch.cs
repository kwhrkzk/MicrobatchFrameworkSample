using MicroBatchFramework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;

namespace MicrobatchFrameworkSample
{
    public class ParamTestBatch : BatchBase
    {
        private ILogger<ParamTestBatch> Logger { get; }
        private Microsoft.Extensions.Hosting.IApplicationLifetime AppLifetime { get; }
        public ParamTestBatch(
            ILogger<ParamTestBatch> _logger,
            Microsoft.Extensions.Hosting.IApplicationLifetime _appLifetime
            )
        {
            Logger = _logger;
            AppLifetime = _appLifetime;

            AppLifetime.ApplicationStarted.Register(OnStarted);
            AppLifetime.ApplicationStopping.Register(OnStopping);
            AppLifetime.ApplicationStopped.Register(OnStopped);
        }

        private void OnStarted()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void BoolParam([Option("x", "説明")]bool x)
        {
            Console.WriteLine(x.ToString());
            Environment.ExitCode = 0;
        }

        public enum MyEnum
        {
            Enum1,
            Enum2
        }

        public void EnumParam([Option("x", "説明")]MyEnum x)
        {
            Console.WriteLine(Enum.GetName(typeof(MyEnum), x));
            Environment.ExitCode = 0;
        }

        public void ListStringParam([Option("x", "説明")]List<string> x)
        {
            x.ForEach(item => Console.WriteLine(item));
            Environment.ExitCode = 0;
        }

        public void ListDoubleParam([Option("x", "説明")]List<double> x)
        {
            x.ForEach(item => Console.WriteLine(item));
            Environment.ExitCode = 0;
        }

        public void ListIntParam([Option("x", "説明")]List<int> x)
        {
            x.ForEach(item => Console.WriteLine(item));
            Environment.ExitCode = 0;
        }

        public class FooBarJsonFormatterResolver : IJsonFormatterResolver
        {
            public static IJsonFormatterResolver Instance = new FooBarJsonFormatterResolver();
            public IJsonFormatter<T> GetFormatter<T>()
            {
                return new FooBar.FooBarJsonFormatter() as IJsonFormatter<T>;
            }
        }

        [JsonFormatter(typeof(FooBarJsonFormatter))]
        public class FooBar
        {
            [DataMember(Name = "foo")]
            public int FooProperty { get; set; }

            [DataMember(Name = "bar")]
            public string BarProperty { get; set; }

            public class FooBarJsonFormatter : IJsonFormatter<FooBar>
            {
                public FooBar Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
                => new FooBar
                    {
                        FooProperty = formatterResolver.GetFormatterWithVerify<int>().Deserialize(ref reader, formatterResolver),
                        BarProperty = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver)
                    };

                public void Serialize(ref JsonWriter writer, FooBar value, IJsonFormatterResolver formatterResolver)
                {
                    formatterResolver.GetFormatterWithVerify<int>().Serialize(ref writer, value.FooProperty, formatterResolver);
                    formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.BarProperty, formatterResolver);
                }
            }
        }

        public void MyClassParam([Option("x", "説明")]FooBar x)
        {
            Console.WriteLine(x.ToString());
            Environment.ExitCode = 0;
        }

        private void OnStopping()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        private void OnStopped()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}

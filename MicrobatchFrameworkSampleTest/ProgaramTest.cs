using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MicrobatchFrameworkSampleTest
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]public void ExecuteTest() => MicrobatchFrameworkSample.Program.Main(new[] { "XXXBatch.Execute" }).Wait();
        [TestMethod]public void BoolParamTest() => MicrobatchFrameworkSample.Program.Main(new[] { "ParamTestBatch.BoolParam", "-x", "false" }).Wait();
        [TestMethod]public void EnumParamTest() => MicrobatchFrameworkSample.Program.Main(new[] { "ParamTestBatch.EnumParam", "-x", "Enum1" }).Wait();
        [TestMethod]public void ListStringParamTest() => MicrobatchFrameworkSample.Program.Main(new[] { "ParamTestBatch.ListStringParam", "-x", "[\"foo\", \"bar\"]" }).Wait();
        [TestMethod]public void ListStringParamTest2() => MicrobatchFrameworkSample.Program.Main(new[] { "ParamTestBatch.ListStringParam", "-x", "\"foo\", \"bar\"" }).Wait();
        [TestMethod]public void ListDoubleParamTest() => MicrobatchFrameworkSample.Program.Main(new[] { "ParamTestBatch.ListDoubleParam", "-x", "[0.5,1000000000000000000,2]" }).Wait();
        [TestMethod]public void ListIntParamTest() => MicrobatchFrameworkSample.Program.Main(new[] { "ParamTestBatch.ListIntParam", "-x", "[0,1,2]" }).Wait();
        [TestMethod]public void MyClassParamTest() => MicrobatchFrameworkSample.Program.Main(new[] { "ParamTestBatch.MyClassParam", "-x", "{\"foo\": 0, \"bar\": \"str\"}" }).Wait();
    }
}

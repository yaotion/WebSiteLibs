using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TF.YA.Org;
using NUnit.Framework.Api;
using NUnit.Framework.Internal;
using NUnit.Framework.Interfaces;

namespace TestForm
{
    public partial class Form1 : Form, ITestListener
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NUnitTestAssemblyRunner Runner = new NUnitTestAssemblyRunner(new DefaultTestAssemblyBuilder());
            var runSettings = new Dictionary<string, object>();
            Runner.Load("TF.YA.Org.Test.dll", runSettings);
            Runner.Run(this, TestFilter.Empty);


            
        }

        /// <summary>
        /// Called when a test has just started
        /// </summary>
        /// <param name="test">The test that is starting</param>
        public void TestStarted(ITest test)
        {
            
        }

        /// <summary>
        /// Called when a test has finished
        /// </summary>
        /// <param name="result">The result of the test</param>
        public void TestFinished(ITestResult result)
        {
            FlushClient fc = new FlushClient(this.ThreadFunction);

            this.BeginInvoke(fc, result);
        }

        /// <summary>
        /// Called when a test produces output for immediate display
        /// </summary>
        /// <param name="output">A TestOutput object containing the text to display</param>
        public void TestOutput(TestOutput output)
        {
            
        }

        private delegate void FlushClient(ITestResult result);//代理


        private void ThreadFunction(ITestResult result)
        {

            string testName = result.Test.Name;

            if (result.Test.IsSuite)
            { }
            else
                switch (result.ResultState.Status)
                {
                    case TestStatus.Passed:
                        textBox1.Text += string.Format("{0}:测试通过，用时{1}秒", testName, result.Duration) + '\r' + '\n';
                        break;
                    case TestStatus.Inconclusive:
                        textBox1.Text += string.Format("{0}:测试不确定结果", testName) + '\r' + '\n';
                        break;
                    case TestStatus.Skipped:
                        textBox1.Text += string.Format("{testName}:被忽略,原因为:{1}", testName, result.Message) + '\r' + '\n';
                        break;
                    case TestStatus.Failed:
                        textBox1.Text += string.Format("{0}:测试失败，用时{1}毫秒,{2},{3}", testName, result.Duration, result.Message, result.StackTrace) + '\r' + '\n';
                        break;
                }

        }

    }
}

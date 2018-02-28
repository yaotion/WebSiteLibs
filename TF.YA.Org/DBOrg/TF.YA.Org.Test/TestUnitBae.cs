using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TF.YA.Org
{
    public class RollbackAttribute : Attribute,ITestAction
    {
        private static TransactionScope transaction;
        public void BeforeTest(ITest test)
        {
            transaction = new TransactionScope();            
        }

        public void AfterTest(ITest test)
        {            
            transaction.Dispose();
    
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }



  


}

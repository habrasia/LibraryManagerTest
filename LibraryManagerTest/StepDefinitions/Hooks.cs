using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerTest.StepDefinitions
{
    [Binding]
    internal sealed class Hooks
    {
        [BeforeTestRun]
        public static void SetUpLibrary()
        {
            
        }

        [AfterTestRun]
        public static void CleanUpLibrary()
        {

        }
    }
}

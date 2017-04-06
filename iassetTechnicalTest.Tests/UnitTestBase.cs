using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iassetTechnicalTest.Tests
{
    public class UnitTestBase
    {
        public UnitTestBase()
        {
            DependencyConfig.SetupDependencies();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlavStore.Models;

namespace SlavStore.Data.Mocks
{
    public class FakeCategorySet:FakeDbSet<Category>
    {
        public override Category Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.Id == (int)keyValues.Single());
        }
    }
}

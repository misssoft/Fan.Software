using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Software.DomainModels
{
    public class GroupWorkViewModel
    {
        public IEnumerable<WorkViewModel> Works { get; set; }

        public string Heading { get; set; }

        public bool ManagingAccount { get; set; }
    }

    public class WorkViewModel
    {
        public GroupWork GroupWork { get; set; }

        public bool CanJoin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VillainNames
{
    class VillainNamesAndMinionsCount
    {
        public string Name { get; set; }
        public int MinionsCount { get; set; }

        public VillainNamesAndMinionsCount(string name, int count)
        {
            this.Name = name;
            this.MinionsCount = count;
        }
    }
}

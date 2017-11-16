using Microsoft.Azure.CosmosDB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTableDemo
{
    class ARow : TableEntity
    {
        public ARow() { }

        public ARow(string aPartitionKey, string aRowKey, int aCount)
        {
            this.PartitionKey = aPartitionKey;
            this.RowKey = aRowKey;
            this.count = aCount;
        }

        public int count { get; set; }
    }
}

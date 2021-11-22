/*
 * Author: FABIAN ISAZA
 * Date: July 04, 2017
 * Description: SuppliersDB
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts
{
    public class Supplier
    {
        private int supplierId;
        private string supName;
        public Supplier() { } //empty constructor

        public int SupplierId { get; set; }
        public string SupName { get; set; }
    }
    //public override string ToString()
    //{
    //    return supplierId.ToString() + ", " + supName;
    //}
}

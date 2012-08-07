using System;
using System.Collections.Generic;


namespace FluencyKoans.Models
{
	public class Order
	{
		public int OrderNumber { get; set; }
		public DateTime OrderDate { get; set; }
		public Customer Customer { get; set; }
		public Decimal OrderTotal { get; set; }
		public IList< LineItem > LineItems { get; set; }
	}
}
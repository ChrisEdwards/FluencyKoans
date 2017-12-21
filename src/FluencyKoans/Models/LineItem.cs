namespace FluencyKoans.Models
{
	public class LineItem
	{
		public int Quantity { get; set; }
		public string Description { get; set; }
		public decimal UnitCost { get; set; }
		public Order Order { get; set; }
	}
}
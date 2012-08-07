using System;


namespace FluencyKoans.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public string HomePhone { get; set; }
		public string WorkPhone { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public DateTime CustomerSinceDate { get; set; }
	}
}
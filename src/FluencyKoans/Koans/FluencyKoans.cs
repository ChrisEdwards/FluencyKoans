using System;
using FluencyKoans.Models;
using FluencyKoans.Utils;
using NUnit.Framework;
using Shouldly;

// ReSharper disable InconsistentNaming


namespace FluencyKoans.Koans
{
	/// <summary>
	/// Fill in the blank ( __ ) in each test with the code that will make the assertions pass.
	/// You can collapse all the hints so you only look at them if you need them. (That's why they are in regions).
	/// </summary>
	[ TestFixture ]
	public class FluencyKoans
	{
		[ Test ]
		public void Using_DynamicFluentBuilder_to_build_an_object()
		{
			/*
			 * Use Fluency's DynamicFluentBuilder<> class to build a Customer object.
			 */


			customer = __;


			// Assert.
			customer.Print();
			customer.FirstName.ShouldNotBeEmpty();
			customer.MiddleName.ShouldNotBeEmpty();
			customer.LastName.ShouldNotBeEmpty();
		}


		[ Test ]
		public void Using_Having_and_With_to_set_property_values()
		{
			/*
			 * Set the Customer's first and last name in the builder.
			 */
			#region Hint

			/*  
			 * Use the With or Having methods. 
			 */

			#endregion Hint


			customer = __;


			// Assert.
			customer.Print();
			customer.FirstName.ShouldBe( "Bob" );
			customer.LastName.ShouldBe( "Smith" );
		}


		[ Test ]
		public void Nesting_builders_by_setting_a_property_to_another_builder()
		{
			/*
			 * Build an Order object for customer Bob Smith.
			 */
			#region Hint

			/*
			 * Use the For() method to set the customer property.
			 * Use another builder to create the customer.
			 * This builder can be nested right inside the Order builder. 
			 * You can assign a builder directly to the property. You don't have to call build().
			 */

			#endregion Hint
			#region Extra Credit

			/*
			 * Delegate the creation of the builder to a static class called "Anonymous".
			 */

			#endregion Extra Credit


			order = __;


			// Assert.
			order.Print();
			order.Customer.ShouldNotBe( null );
			order.Customer.FirstName.ShouldBe( "Bob" );
			order.Customer.LastName.ShouldBe( "Smith" );
		}


		[ Test ]
		public void Creating_a_line_item()
		{
			/*
			 * Build a LineItem (yeah it's easy, but it set's us up for the next one.). 
			 */


			lineItem = __;


			// Assert.
			lineItem.Print();
			lineItem.Description.ShouldNotBeEmpty();
		}


		[ Test ]
		public void Building_collections_using_HavingListOf()
		{
			/*
			 * Build an Order that contains two LineItems.
			 */
			#region Hint

			/*
			 * Use HavingListOf() method on the builder to assign two line items to the order.
			 */

			#endregion Hint


			order = __;


			// Assert.
			order.Print();
			order.Customer.ShouldNotBe( null );
			order.LineItems.ShouldNotBe( null );
			order.LineItems.Count.ShouldBe( 2 );
			order.LineItems[0].Description.ShouldNotBeEmpty();
			order.LineItems[1].Description.ShouldNotBeEmpty();
		}


		[ Test ]
		public void Using_a_custom_builder_to_customize_object_creation()
		{
			/*
			 * Create a custom OrderBuilder so that by default, it builds an Order with a Customer and two LineItems.
			 */
			#region Hint

			/*
			 * Create a new class that extends DynamicFluentBuilder<Customer>.
			 * Set the default values in the constructor of the class.
			 */

			#endregion Hint


			order = __;


			// Assert.
			order.Print();
			order.Customer.ShouldNotBe( null );
			order.LineItems.ShouldNotBe( null );
			order.LineItems.Count.ShouldBe( 2 );
			order.LineItems[0].Description.ShouldNotBeEmpty();
			order.LineItems[1].Description.ShouldNotBeEmpty();
		}


		[ Test ]
		public void Using_AfterBuild_to_customize_the_object_after_it_is_built()
		{
			/*
			 * Ensure that each LineItem in an Order has a reference to the Order.
			 */
			#region Hint

			/*
			 * Iterate through line items and set thier order property.
			 * Put this code in builder.AfterBuild() to ensure it runs after each object is build.
			 */

			#endregion Hint


			order = __;


			// Assert.
			order.Print();
			order.LineItems[0].Order.ShouldBe( order );
			order.LineItems[1].Order.ShouldBe( order );
		}


		[ Test ]
		public void Customizing_the_builder_to_satisfy_invariants()
		{
			/*
			 * Create a customer builder that ensures that:
			 *   (1) The CustomerSinceDate is greater than the Birthdate, and 
			 *   (2) Both dates are in the past.
			 */
			#region Hint

			/*
			 * Create a CustomerBuilder and set these values in its constructor.
			 * Use GetValue( x => x.BirthDate) to retrieve the birthdate so you can generate a later CustomerSinceDate.
			 */

			#endregion Hint

			#region Extra Credit

			/*
			 * Ensure that customers must be 18 or older.
			 */

			#endregion Extra Credit
			#region Extra Credit Hint

			/*
			 * Generate an age >18 and reverse engineer a birthdate from that.
			 * Generate a CustSinceDate that is after their 18th birthday.
			 */

			#endregion Extra Credit Hint


			customer = __;


			// Assert.
			customer.Print();
			customer.BirthDate.ShouldBeLessThan( DateTime.Now );
			customer.CustomerSinceDate.ShouldBeLessThan( DateTime.Now );
			customer.CustomerSinceDate.ShouldBeGreaterThan( customer.BirthDate );
			// Extra Credit. (uncomment the following line to test it)
			// customer.CustomerSinceDate.ShouldBeGreaterThan( customer.BirthDate.AddYears( 18 ) );
		}


		[ Test ]
		public void Adding_custom_methods_to_the_builder_builder_to_increase_expressiveness()
		{
			/*
			 * Modify the CustomerBuilder to easily specify the customer is a minor (under 21).
			 */
			#region Hint

			/*
			 * Create CustomerBuilder.WhoIsAMinor() method to generate the birthdate for someone under 21.
			 */

			#endregion Hint


			customer = __;


			// Assert.
			customer.Print();
			var age = YearsBetween( customer.BirthDate, DateTime.Now );
			age.ShouldBeLessThan( 21 );
		}


		#region privates

		/// <summary>
		/// Calculated the number of years between two dates. Warning...not very precise since it assumes 365 days per year and does not consider leap years.
		/// </summary>
		/// <param name="earlierDate">The earlier date.</param>
		/// <param name="laterDate">The later date.</param>
		/// <returns></returns>
		private static double YearsBetween( DateTime earlierDate, DateTime laterDate )
		{
			var age = ( laterDate.Subtract( earlierDate ).TotalDays / 365 );
			return age;
		}


		private Order order;
		private Customer customer;
		private LineItem lineItem;
		private dynamic __;

		#endregion privates
	}
}


// ReSharper restore InconsistentNaming
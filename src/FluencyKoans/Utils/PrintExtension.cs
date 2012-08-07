using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fluency.Utils;


namespace FluencyKoans.Utils
{
	public static class PrintExtension
	{
		/// <summary>
		/// Prints the specified object to the console as nested JSON.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objectToPrint">The object to print.</param>
		public static void Print< T >( this T objectToPrint ) where T : class
		{
			Console.WriteLine( objectToPrint.ToPrintableString() );
		}


		/// <summary>
		/// Generates a dynamic string showing the type of the class and all its properties using reflection. String is in the form:<br/>
		/// <code>{{ TypeName { Property1 = [ Value1 ]; Property2 = [ Value2 ]; ... PropertyN = [ Value N ]; } }}</code>
		/// </summary>
		/// <typeparam name="T">The type of object we are wanting to print.</typeparam>
		/// <param name="objectToPrint">The object to print.</param>
		/// <returns>String describing the object.</returns>
		public static string ToPrintableString< T >( this T objectToPrint ) where T : class
		{
			var type = typeof ( T );

			return GetPrintableString( objectToPrint, type );
		}


		private static int indentCount;
		private const int maxDepth = 3;
		private static int depth;


		private static string GetPrintableString( object objectToPrint, Type type )
		{
			if ( objectToPrint == null )
				return string.Empty;

			if ( depth >= maxDepth )
				return objectToPrint.ToString();

			if ( IsGenericListType( type ) )
				return GetEnumerablePrintableString( objectToPrint, type );

			if ( type.IsValueType || type.FullName.Contains( "System" ) || type.FullName.Contains( "Microsoft" ) )
				return objectToPrint.ToString();

			depth++;

			var sb = new StringBuilder();

			sb.AppendFormat( "{0}{1} {{ ",
			                 Indent( indentCount ),
			                 type.Name );

			indentCount++;
			foreach ( var property in type.GetPublicGetProperties() )
			{
				sb.AppendFormat( "\n{0}{1} = [ {2} ]; ",
				                 Indent( indentCount ),
				                 property.Name,
				                 GetPrintableString( property.GetValue( objectToPrint, null ), property.PropertyType ) );
			}
			indentCount--;

			sb.AppendFormat( "\n{0}}}", Indent( indentCount ) );

			depth--;

			return sb.ToString();
		}


		private static string GetEnumerablePrintableString( object objectToPrint, Type type )
		{
			if ( !IsGenericListType( type ) )
				throw new ArgumentException( "Illegal Argument Type: Expected IEnumerable<> but was " + type.Name );

			var listItemType = type.GetGenericArguments().First();

			var itemCounter = 1;
			var sb = new StringBuilder();

			indentCount++;
			foreach ( var item in (IEnumerable)objectToPrint )
			{
				sb.AppendFormat( "\n{0}#{1}: \n",
				                 Indent( indentCount ),
				                 itemCounter++ );

				indentCount++;
				sb.Append( GetPrintableString( item, listItemType ) );
				indentCount--;
			}
			indentCount--;

			return sb.ToString();
		}


		public static bool IsGenericListType( Type type )
		{
			return type.Name == "List`1" || type.Name == "IList`1";
			foreach ( var interfaceType in type.GetInterfaces() )
			{
				if ( interfaceType.IsGenericType )
				{
					if ( interfaceType.GetGenericTypeDefinition() == typeof ( IList< > ) )
						return true;
				}
			}
			return false;
		}


		private static string Indent( int count )
		{
			var sb = new StringBuilder( count );
			for ( var i = 0; i < count; i++ )
				sb.Append( "\t\t" );
			return sb.ToString();
		}
	}
}
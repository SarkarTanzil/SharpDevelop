﻿/*
 * Created by SharpDevelop.
 * User: Peter Forstmeier
 * Date: 13.06.2013
 * Time: 11:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ICSharpCode.Reporting.DataManager.Listhandling;
using ICSharpCode.Reporting.Factories;
using ICSharpCode.Reporting.Interfaces;
using ICSharpCode.Reporting.Interfaces.Export;
using ICSharpCode.Reporting.Items;
using ICSharpCode.Reporting.PageBuilder.ExportColumns;

namespace ICSharpCode.Reporting.PageBuilder.Converter
{
	/// <summary>
	/// Description of DataContainerConverter.
	/// </summary>

	internal class DataContainerConverter:ContainerConverter
	{
		

		private CollectionSource collectionSource;
		
		public DataContainerConverter(Graphics graphics, IReportContainer reportContainer,
		                              Point currentLocation,CollectionSource collectionSource):base(graphics,reportContainer,currentLocation)
		{
			if (graphics == null) {
				throw new ArgumentNullException("graphics");
			}
			if (reportContainer == null) {
				throw new ArgumentNullException("reportContainer");
			}
			this.collectionSource = collectionSource;
		}
		
		
		public override IExportContainer Convert(){
			if (collectionSource.Count == 0) {
				return base.Convert();
			}
	
			var exportContainer = CreateExportContainer();
			
			do {
				collectionSource.Fill(Container.Items);
				Console.WriteLine(((BaseDataItem)Container.Items[0]).DBValue);
				var itemsList = CreateConvertedList(exportContainer);
				exportContainer.ExportedItems.AddRange(itemsList);
			}
			while (collectionSource.MoveNext());
			
//			Console.WriteLine("calling Container-Arrange");
//			var exportArrange = exportContainer.GetArrangeStrategy();
//			exportArrange.Arrange(exportContainer);
			ArrangeContainer(exportContainer);
			return exportContainer;
		}
	}
}

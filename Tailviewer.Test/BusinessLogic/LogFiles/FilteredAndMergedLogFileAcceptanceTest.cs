﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Tailviewer.BusinessLogic.Filters;
using Tailviewer.BusinessLogic.LogFiles;

namespace Tailviewer.Test.BusinessLogic.LogFiles
{
	[TestFixture]
	public sealed class FilteredAndMergedLogFileAcceptanceTest
	{
		[Test]
		[Ignore]
		public void Test()
		{
			using (var source1 = new LogFile(LogFileAcceptanceTest.File2Entries))
			using (var source2 = new LogFile(LogFileAcceptanceTest.File2Lines))
			{
				var sources = new List<ILogFile> {source1, source2};
				using (var merged = new MergedLogFile(sources))
				{
					var filter = new SubstringFilter("foo", true);
					using (var filtered = new FilteredLogFile(merged, filter))
					{
						source1.Start();
						source2.Start();
						merged.Start(TimeSpan.FromMilliseconds(10));
						filtered.Start(TimeSpan.FromMilliseconds(10));

						filtered.Property(x => x.Count).ShouldEventually().Be(1, TimeSpan.FromSeconds(5));
					}
				}
			}
		}
	}
}
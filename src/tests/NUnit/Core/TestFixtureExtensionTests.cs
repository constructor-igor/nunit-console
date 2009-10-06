// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core.Builders;
using NUnit.TestUtilities;
using NUnit.TestData.TestFixtureExtensionData;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for TestFixtureExtension.
	/// </summary>
	/// 
	[TestFixture]
	public class TestFixtureExtensionTests
	{
		private Test suite;

		private void RunTestOnFixture( object fixture )
		{
			Test suite = TestBuilder.MakeFixture( fixture );
            suite.Run(NullListener.NULL, TestFilter.Empty);
		}

		[SetUp] public void LoadFixture()
		{
			string testsDll = "test-assembly.dll";
			TestSuiteBuilder builder = new TestSuiteBuilder();
			TestPackage package = new TestPackage( testsDll );
			package.TestName = "NUnit.TestData.TestFixtureExtensionData.DerivedTestFixture";
			suite= builder.Build( package );
		}

		[Test] 
		public void CheckMultipleSetUp()
		{
			SetUpDerivedTestFixture fixture = new SetUpDerivedTestFixture();
			RunTestOnFixture( fixture );

			Assert.AreEqual(true, fixture.baseSetup);		}

		[Test]
		public void DerivedTest()
		{
			Assert.IsNotNull(suite);

            TestResult result = suite.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsSuccess);
		}

		[Test]
		public void InheritSetup()
		{
			DerivedTestFixture fixture = new DerivedTestFixture();
			RunTestOnFixture( fixture );

			Assert.AreEqual(true, fixture.baseSetup);
		}

		[Test]
		public void InheritTearDown()
		{
			DerivedTestFixture fixture = new DerivedTestFixture();
			RunTestOnFixture( fixture );

			Assert.AreEqual(true, fixture.baseTeardown);
		}
	}
}

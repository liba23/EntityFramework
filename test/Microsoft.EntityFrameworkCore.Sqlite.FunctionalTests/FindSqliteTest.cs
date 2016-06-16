// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Sqlite.FunctionalTests
{
    public class FindSqliteTest
        : FindTestBase<FindSqliteTest.FindSqliteFixture>
    {
        public FindSqliteTest(FindSqliteFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public override void Find_int_key_tracked()
        {
            base.Find_int_key_tracked();

            Assert.Equal("", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_int_key_from_store()
        {
            base.Find_int_key_from_store();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Foo""
FROM ""IntKey"" AS ""e""
WHERE ""e"".""Id"" = 77
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Returns_null_for_int_key_not_in_store()
        {
            base.Returns_null_for_int_key_not_in_store();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Foo""
FROM ""IntKey"" AS ""e""
WHERE ""e"".""Id"" = 99
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_string_key_tracked()
        {
            base.Find_string_key_tracked();

            Assert.Equal("", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_string_key_from_store()
        {
            base.Find_string_key_from_store();

            Assert.Equal(@"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Foo""
FROM ""StringKey"" AS ""e""
WHERE ""e"".""Id"" = 'Cat'
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Returns_null_for_string_key_not_in_store()
        {
            base.Returns_null_for_string_key_not_in_store();

            Assert.Equal(@"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Foo""
FROM ""StringKey"" AS ""e""
WHERE ""e"".""Id"" = 'Fox'
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_composite_key_tracked()
        {
            base.Find_composite_key_tracked();

            Assert.Equal("", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_composite_key_from_store()
        {
            base.Find_composite_key_from_store();

            Assert.Equal(@"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id1"", ""e"".""Id2"", ""e"".""Foo""
FROM ""CompositeKey"" AS ""e""
WHERE (""e"".""Id1"" = 77) AND (""e"".""Id2"" = 'Dog')
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Returns_null_for_composite_key_not_in_store()
        {
            base.Returns_null_for_composite_key_not_in_store();

            Assert.Equal(@"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id1"", ""e"".""Id2"", ""e"".""Foo""
FROM ""CompositeKey"" AS ""e""
WHERE (""e"".""Id1"" = 77) AND (""e"".""Id2"" = 'Fox')
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_base_type_tracked()
        {
            base.Find_base_type_tracked();

            Assert.Equal("", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_base_type_from_store()
        {
            base.Find_base_type_from_store();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Discriminator"", ""e"".""Foo"", ""e"".""Boo""
FROM ""BaseType"" AS ""e""
WHERE ""e"".""Discriminator"" IN ('DerivedType', 'BaseType') AND (""e"".""Id"" = 77)
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Returns_null_for_base_type_not_in_store()
        {
            base.Returns_null_for_base_type_not_in_store();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Discriminator"", ""e"".""Foo"", ""e"".""Boo""
FROM ""BaseType"" AS ""e""
WHERE ""e"".""Discriminator"" IN ('DerivedType', 'BaseType') AND (""e"".""Id"" = 99)
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_derived_type_tracked()
        {
            base.Find_derived_type_tracked();

            Assert.Equal("", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_derived_type_from_store()
        {
            base.Find_derived_type_from_store();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Discriminator"", ""e"".""Foo"", ""e"".""Boo""
FROM ""BaseType"" AS ""e""
WHERE (""e"".""Discriminator"" = 'DerivedType') AND (""e"".""Id"" = 78)
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Returns_null_for_derived_type_not_in_store()
        {
            base.Returns_null_for_derived_type_not_in_store();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Discriminator"", ""e"".""Foo"", ""e"".""Boo""
FROM ""BaseType"" AS ""e""
WHERE (""e"".""Discriminator"" = 'DerivedType') AND (""e"".""Id"" = 99)
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_base_type_using_derived_set_tracked()
        {
            base.Find_base_type_using_derived_set_tracked();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Discriminator"", ""e"".""Foo"", ""e"".""Boo""
FROM ""BaseType"" AS ""e""
WHERE (""e"".""Discriminator"" = 'DerivedType') AND (""e"".""Id"" = 88)
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_base_type_using_derived_set_from_store()
        {
            base.Find_base_type_using_derived_set_from_store();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Discriminator"", ""e"".""Foo"", ""e"".""Boo""
FROM ""BaseType"" AS ""e""
WHERE (""e"".""Discriminator"" = 'DerivedType') AND (""e"".""Id"" = 77)
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_derived_type_using_base_set_tracked()
        {
            base.Find_derived_type_using_base_set_tracked();

            Assert.Equal("", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_derived_using_base_set_type_from_store()
        {
            base.Find_derived_using_base_set_type_from_store();

            Assert.Equal(
                @"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Discriminator"", ""e"".""Foo"", ""e"".""Boo""
FROM ""BaseType"" AS ""e""
WHERE ""e"".""Discriminator"" IN ('DerivedType', 'BaseType') AND (""e"".""Id"" = 78)
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_shadow_key_tracked()
        {
            base.Find_shadow_key_tracked();

            Assert.Equal("", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Find_shadow_key_from_store()
        {
            base.Find_shadow_key_from_store();

            Assert.Equal(@"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Foo""
FROM ""ShadowKey"" AS ""e""
WHERE ""e"".""Id"" = 77
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        [Fact]
        public override void Returns_null_for_shadow_key_not_in_store()
        {
            base.Returns_null_for_shadow_key_not_in_store();

            Assert.Equal(@"PRAGMA foreign_keys=ON;

SELECT ""e"".""Id"", ""e"".""Foo""
FROM ""ShadowKey"" AS ""e""
WHERE ""e"".""Id"" = 99
LIMIT 1", TestSqlLoggerFactory.Sql);
        }

        public override void Dispose() => TestSqlLoggerFactory.Reset();

        public class FindSqliteFixture : FindFixtureBase
        {
            private readonly IServiceProvider _serviceProvider;

            public FindSqliteFixture()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlite()
                    .AddSingleton(TestSqliteModelSource.GetFactory(OnModelCreating))
                    .AddSingleton<ILoggerFactory, TestSqlLoggerFactory>()
                    .BuildServiceProvider();
            }

            public override void CreateTestStore()
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    Seed(context);
                    TestSqlLoggerFactory.Reset();
                }
            }

            public override DbContext CreateContext()
                => new FindContext(new DbContextOptionsBuilder()
                    .UseSqlite(SqliteTestStore.CreateConnectionString("FindTest"))
                    .UseInternalServiceProvider(_serviceProvider).Options);
        }
    }
}

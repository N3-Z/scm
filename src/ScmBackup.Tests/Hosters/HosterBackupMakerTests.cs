﻿using ScmBackup.CompositionRoot;
using ScmBackup.Hosters;
using System;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class HosterBackupMakerTests
    {
        [Fact]
        public void MakeBackupCallsUnderlyingMethod()
        {
            var hoster = new FakeHoster();

            var factory = new FakeHosterFactory(hoster);
            var repo = new HosterRepository("foo", "foo", "http://clone", ScmType.Git);
            repo.SetWiki(true, "http://wiki");
            repo.SetIssues(true, "http://issues");

            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var config = reader.ReadConfig();
            var source = config.Sources.First();

            var sut = new HosterBackupMaker(factory);
            sut.MakeBackup(source, repo, "foo");

            Assert.True(hoster.FakeBackup.WasExecuted);
        }

        [Fact]
        public void ThrowsWhenConfigSourceIsNull()
        {
            var factory = new FakeHosterFactory(new FakeHoster());
            var repo = new HosterRepository("foo", "foo", "http://clone", ScmType.Git);

            var sut = new HosterBackupMaker(factory);
            Assert.Throws<ArgumentNullException>(() => sut.MakeBackup(null, repo, "foo"));
        }
    }
}

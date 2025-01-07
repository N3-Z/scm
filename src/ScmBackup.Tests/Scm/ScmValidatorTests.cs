﻿using ScmBackup.Scm;
using ScmBackup.Tests.Scm;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScmBackup.Tests
{
    public class ScmValidatorTests
    {
        private FakeScmFactory factory;
        private FakeLogger logger;

        private HashSet<ScmType> scmlist;
        private FakeScm scm;

        private IScmValidator sut;

        public ScmValidatorTests()
        {
            this.logger = new FakeLogger();

            this.scmlist = new HashSet<ScmType>();
            this.scmlist.Add(ScmType.Git);

            this.scm = new FakeScm();

            this.factory = new FakeScmFactory();
            this.factory.Register(ScmType.Git, this.scm);

            this.sut = new ScmValidator(this.factory, this.logger);
        }

        [Fact]
        public void ReturnsTrueWhenScmsValidate()
        {
            this.scm.IsOnThisComputerResult = true;

            var result = this.sut.ValidateScms(this.scmlist);

            Assert.True(result);
            Assert.NotEqual(ErrorLevel.Error, this.logger.LastErrorLevel);
        }

        [Fact]
        public void ReturnsFalseWhenScmsDontValidate()
        {
            this.scm.IsOnThisComputerResult = false;

            var result = this.sut.ValidateScms(this.scmlist);

            Assert.False(result);
            Assert.Equal(ErrorLevel.Error, this.logger.LastErrorLevel);
        }

        [Fact]
        public void DoesNotThrowExceptionAndReturnsFalseWhenScmThrowsException()
        {
            this.scm.IsOnThisComputerException = new Exception("boom");

            var result = this.sut.ValidateScms(this.scmlist);

            Assert.False(result);
            Assert.Equal(ErrorLevel.Error, this.logger.LastErrorLevel);
        }
    }
}

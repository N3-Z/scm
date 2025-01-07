﻿using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScmBackup
{
    internal class DeletedRepoHandler : IDeletedRepoHandler
    {
        private readonly ILogger logger;
        private readonly IFileSystemHelper fileHelper;
        private readonly IContext context;

        public DeletedRepoHandler(ILogger logger, IFileSystemHelper fileHelper, IContext context)
        {
            this.logger = logger;
            this.fileHelper = fileHelper;
            this.context = context;
        }

        public IEnumerable<string> HandleDeletedRepos(IEnumerable<HosterRepository> repos, string backupDir)
        {
            var alldirs = this.fileHelper.GetSubDirectoryNames(backupDir);
            var repodirs = repos.Select(x => x.FullName);

            var deletedRepoDirs = alldirs.Except(repodirs);

            if (deletedRepoDirs.Any())
            {
                bool remove = this.context.Config.Options.Backup.RemoveDeletedRepos;

                if (remove)
                {
                    this.logger.Log(ErrorLevel.Warn, Resource.DeletedRepoRemoving);
                }
                else
                {
                    this.logger.Log(ErrorLevel.Warn, Resource.DeletedRepoWarning);
                }

                foreach (string dir in deletedRepoDirs)
                {
                    if (remove)
                    {
                        this.fileHelper.DeleteDirectory(this.fileHelper.PathCombine(backupDir, dir));
                    }

                    this.logger.Log(ErrorLevel.Warn, "  " + dir);
                }
            }

            return deletedRepoDirs;
        }
    }
}

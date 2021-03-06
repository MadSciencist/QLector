﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace QLector.Application.Core.Infrastructure
{
    internal class ModuleLoader
    {
        private readonly IHostAssemblyProvider _hostAssemblyProvider;

        public ModuleLoader(IHostAssemblyProvider hostAssemblyProvider)
        {
            _hostAssemblyProvider = hostAssemblyProvider;
        }

        internal IEnumerable<IApplicationModule> GetModules()
        {
            var modules = new List<IApplicationModule>();
            var binDirectory = new FileInfo(_hostAssemblyProvider.GetEntryAssembly().Location).DirectoryName;

            if (!Directory.Exists(binDirectory))
                return modules;

            var dirInfo = new DirectoryInfo(binDirectory);
            var libs = dirInfo.GetFileSystemInfos("*.dll", SearchOption.TopDirectoryOnly);

            foreach (var lib in libs)
            {
                try
                {
                    var moduleAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(lib.FullName);
                    var moduleDefinitionTypes = moduleAssembly.GetTypes().Where(x =>
                        typeof(IApplicationModule).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface).ToList();

                    if (moduleDefinitionTypes.Any())
                    {
                        foreach (var moduleType in moduleDefinitionTypes)
                        {
                            var appModuleInstance = Activator.CreateInstance(moduleType) as IApplicationModule;
                            modules.Add(appModuleInstance);
                        }
                    }
                }
                catch (FileLoadException)
                {
                    // Swallow?
                }
            }

            return modules;
        }
    }
}

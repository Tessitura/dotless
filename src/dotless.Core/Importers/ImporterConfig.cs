namespace dotless.Core.Importers
{
    using dotless.Core.configuration;

    public class ImporterConfig
    {
        public bool DisableUrlRewriting { get; set; }

        public string RootPath { get; set; }

        public bool InlineCssFiles { get; set; }

        public bool ImportAllFilesAsLess { get; set; }

        internal ImporterConfig()
        { }

        public ImporterConfig(DotlessConfiguration config)
        {
            DisableUrlRewriting = config.DisableUrlRewriting;
            RootPath = config.RootPath;
            InlineCssFiles = config.InlineCssFiles;
            ImportAllFilesAsLess = config.ImportAllFilesAsLess;
        }
    }
}
namespace dotless.Core.Engine
{
    using System.Collections.Generic;

    using dotless.Core.configuration;
    using dotless.Core.Plugins;

    public class LessEngineConfig
    {
        public bool Compress { get; set; }

        public bool Debug { get; set; }

        public bool DisableVariableRedefines { get; set; }

        public bool DisableColorCompression { get; set; }

        public bool KeepFirstSpecialComment { get; set; }

        public bool StrictMath { get; set; }

        public List<IPluginConfigurator> Plugins { get; set; }

        public LessEngineConfig(DotlessConfiguration config)
        {
            Plugins = config.Plugins;
            Compress = config.MinifyOutput;
            Debug = config.Debug;
            DisableVariableRedefines = config.DisableVariableRedefines;
            DisableColorCompression = config.DisableColorCompression;
            KeepFirstSpecialComment = config.KeepFirstSpecialComment;
            StrictMath = config.StrictMath;
        }
    }
}

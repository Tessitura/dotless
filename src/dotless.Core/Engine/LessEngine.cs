using dotless.Core.Parser;
using dotless.Core.Parser.Tree;
using dotless.Core.Plugins;
using dotless.Core.Stylizers;

namespace dotless.Core
{
    using System.Collections.Generic;
    using System.Linq;

    using dotless.Core.Engine;

    using Exceptions;
    using Loggers;
    using Parser.Infrastructure;

    public class LessEngine : ILessEngine
    {
        public Parser.Parser Parser { get; set; }
        public ILogger Logger { get; set; }
        public bool Compress { get; set; }
        public bool Debug { get; set; }
        public bool DisableVariableRedefines { get; set; }
        public bool DisableColorCompression { get; set; }
        public bool KeepFirstSpecialComment { get; set; }
        public bool StrictMath { get; set; }
        public Env Env { get; set; }
        public IEnumerable<IPluginConfigurator> Plugins { get; set; }
        public bool LastTransformationSuccessful { get; private set; }

        public string CurrentDirectory
        {
            get { return Parser.CurrentDirectory; }
            set { Parser.CurrentDirectory = value; }
        }

        public LessEngine(Parser.Parser parser, ILogger logger, LessEngineConfig config)
        {
            Parser = parser;
            Logger = logger;
            Compress = config.Compress;
            Debug = config.Debug;
            DisableVariableRedefines = config.DisableVariableRedefines;
            Plugins = config.Plugins;
            KeepFirstSpecialComment = config.KeepFirstSpecialComment;
            DisableColorCompression = config.DisableColorCompression;
            StrictMath = config.StrictMath;
        }

        public string TransformToCss(string source, string fileName)
        {
            try
            {
                Parser.StrictMath = StrictMath;
                var tree = Parser.Parse(source, fileName);

                var env = Env ??
                          new Env(Parser)
                              {
                                  Compress = Compress,
                                  Debug = Debug,
                                  KeepFirstSpecialComment = KeepFirstSpecialComment,
                                  DisableVariableRedefines = DisableVariableRedefines,
                                  DisableColorCompression = DisableColorCompression
                              };

                if (Plugins != null)
                {
                    foreach (IPluginConfigurator configurator in Plugins)
                    {
                        env.AddPlugin(configurator.CreatePlugin());
                    }
                }

                var css = tree.ToCSS(env);

                var stylizer = new PlainStylizer();

                foreach (var unmatchedExtension in env.FindUnmatchedExtensions()) {
                    Logger.Warn("Warning: extend '{0}' has no matches {1}\n",
                        unmatchedExtension.BaseSelector.ToCSS(env).Trim(),
                        stylizer.Stylize(new Zone(unmatchedExtension.Extend.Location)).Trim());
                }

                tree.Accept(DelegateVisitor.For<Media>(m => {
                    foreach (var unmatchedExtension in m.FindUnmatchedExtensions()) {
                        Logger.Warn("Warning: extend '{0}' has no matches {1}\n",
                            unmatchedExtension.BaseSelector.ToCSS(env).Trim(),
                            stylizer.Stylize(new Zone(unmatchedExtension.Extend.Location)).Trim());
                    }
                }));

                LastTransformationSuccessful = true;
                return css;
            }
            catch (ParserException e)
            {
                LastTransformationSuccessful = false;
                LastTransformationError = e;
                Logger.Error(e.Message);
            }

            return "";
        }

        public ParserException LastTransformationError { get; set; }

        public IEnumerable<string> GetImports()
        {
            return Parser.Importer.Imports.Distinct();
        }

        public void ResetImports()
        {
            Parser.Importer.Imports.Clear();
        }

    }
}
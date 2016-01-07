namespace dotless.Core
{
    using configuration;

    public static class Less
    {
        public static string Parse(ILessEngine engine, string less)
        {
            return Parse(engine, less, DotlessConfiguration.GetDefault());
        }

        public static string Parse(ILessEngine engine, string less, DotlessConfiguration config)
        {
            return engine.TransformToCss(less, null);
        }
    }
}
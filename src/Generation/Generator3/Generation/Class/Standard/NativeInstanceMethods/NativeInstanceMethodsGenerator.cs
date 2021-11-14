﻿namespace Generator3.Generation.Class.Standard
{
    public class NativeInstanceMethodsGenerator
    {
        private readonly Template<NativeInstanceMethodsModel> _template;
        private readonly Publisher _publisher;

        public NativeInstanceMethodsGenerator(Template<NativeInstanceMethodsModel> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Class @class)
        {
            try
            {
                var model = new NativeInstanceMethodsModel(@class);
                var source = _template.Render(model);
                var codeUnit = new CodeUnit(@class.Namespace.GetCanonicalName(), $"{@class.Name}.Instance.Methods", source);
                _publisher.Publish(codeUnit);
            }
            catch
            {
                Log.Warning($"Could not generate native fundamental class instance methods \"{@class.Name}\"");
                throw;
            }
        }
    }
}

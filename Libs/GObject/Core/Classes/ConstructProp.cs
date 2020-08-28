using System;

namespace GObject
{
    public sealed class ConstructProp
    {
        public string Name { get; }
        public Sys.Value Value { get; }

        private ConstructProp(string name, object? value)
        {
            Name = name;
            Value = Sys.Value.From(value);
        }

        public static ConstructProp With<T>(Property<T> property, T value) => new ConstructProp(property.Name, value);
    }
}
﻿namespace Generator3.Generation.Model
{
    public abstract class ReturnValue
    {
        protected readonly GirModel.ReturnValue _returnValue;

        public abstract string NullableTypeName { get; }
        public string Code => NullableTypeName; //TODO: Remove this property it belongs into the rendering part

        protected ReturnValue(GirModel.ReturnValue returnValue)
        {
            _returnValue = returnValue;
        }

        protected string GetDefaultNullable() => _returnValue.Nullable ? "?" : "";

        public static ReturnValue CreateNative(GirModel.ReturnValue returnValue) => returnValue switch
        {
            { Type: GirModel.String } => new Native.StringReturnValue(returnValue),
            _ => new Native.StandardReturnValue(returnValue)
        };
    }
}

﻿using System;

namespace Generator3.Generation.Model
{
    public abstract class Parameter
    {
        public GirModel.Parameter Model { get; }

        public string Name => Model.Name;
        public abstract string NullableTypeName { get; }
        public abstract string Direction { get; }
        public string Code => $@"{ Direction }{ NullableTypeName } { Name }"; //TODO: Remove this property it belongs into the rendering part

        protected internal Parameter(GirModel.Parameter parameter)
        {
            Model = parameter;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";

        public static Parameter CreateNative(GirModel.Parameter parameter) => parameter switch
        {
            { Type: GirModel.String } => new Native.StringParameter(parameter),
            { Type: GirModel.Pointer } => new Native.PointerParameter(parameter),
            { Type: GirModel.Class } => new Native.ClassParameter(parameter),
            { Type: GirModel.Record } => new Native.RecordParameter(parameter),
            { Type: GirModel.PrimitiveValueType } => new Native.StandardParameter(parameter),
            { Type: GirModel.Callback } => new Native.CallbackParameter(parameter),
            { Type: GirModel.Enumeration } => new Native.StandardParameter(parameter),
            { Type: GirModel.Bitfield } => new Native.StandardParameter(parameter),
            _ => throw new Exception($"Unknown parameter type {parameter.Type.GetType().FullName}")
        };
    }
}

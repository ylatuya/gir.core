﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Internal
{
    public static class ParameterFactory
    {
        public static IEnumerable<Parameter> CreateInternalModels(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateInternalModel);

        private static Parameter CreateInternalModel(this GirModel.Parameter parameter) => parameter.AnyTypeReference.AnyType.Match(
            type => type switch
            {
                GirModel.String => new StringParameter(parameter),
                GirModel.Pointer => new PointerParameter(parameter),
                GirModel.UnsignedPointer => new UnsignedPointerParameter(parameter),
                GirModel.Class => new ClassParameter(parameter),
                GirModel.Interface => new InterfaceParameter(parameter),
                GirModel.Union => new UnionParameter(parameter),
                GirModel.Record => new SafeHandleRecordParameter(parameter),
                GirModel.PrimitiveValueType => new StandardParameter(parameter),
                GirModel.Callback => new CallbackParameter(parameter),
                GirModel.Enumeration => new StandardParameter(parameter),
                GirModel.Bitfield => new StandardParameter(parameter),
                GirModel.Void => new VoidParameter(parameter),
                
                _ => throw new Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyTypeReference.AnyType} can not be converted into a model")
            },
            arrayType => arrayType.AnyTypeReference.AnyType.Match<Parameter>(
                type => type switch
                {
                    GirModel.Record when arrayType.AnyTypeReference.IsPointer => new ArrayPointerRecordParameter(parameter),
                    GirModel.Record => new ArrayRecordParameter(parameter),
                    GirModel.String => new ArrayStringParameter(parameter),
                    _ => new StandardParameter(parameter)
                },
                _ => throw new NotSupportedException("Arrays of arrays not yet supported")
            )
        );
    }
}

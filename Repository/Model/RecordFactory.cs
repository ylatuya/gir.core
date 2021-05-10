﻿using System;

namespace Repository.Model
{
    internal class RecordFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly FieldFactory _fieldFactory;

        public RecordFactory(TypeReferenceFactory typeReferenceFactory, MethodFactory methodFactory, FieldFactory fieldFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _methodFactory = methodFactory;
            _fieldFactory = fieldFactory;
        }

        public Record Create(Xml.Record @record, Namespace @namespace)
        {
            if (@record.Name is null)
                throw new Exception("Record is missing a name");

            Method? getTypeFunction = @record.GetTypeFunction switch
            {
                { } f => _methodFactory.CreateGetTypeMethod(f),
                _ => null
            };

            CTypeName? cTypeName = null;
            if (@record.CType is { })
                cTypeName = new CTypeName(@record.CType);

            return new Record(
                @namespace: @namespace,
                cTypeName: cTypeName,
                typeName: new TypeName(@record.Name),
                symbolName: new SymbolName(@record.Name),
                gLibClassStructFor: GetGLibClassStructFor(@record.GLibIsGTypeStructFor, @namespace.Name),
                methods: _methodFactory.Create(@record.Methods, @namespace),
                functions: _methodFactory.Create(@record.Functions, @namespace),
                getTypeFunction: getTypeFunction,
                fields: _fieldFactory.Create(@record.Fields, @namespace),
                disguised: @record.Disguised,
                constructors: _methodFactory.Create(@record.Constructors, @namespace)
            );
        }

        private TypeReference? GetGLibClassStructFor(string? classStructForName, NamespaceName namespaceName)
        {
            TypeReference? getGLibClassStructFor = null;

            if (classStructForName is { })
                getGLibClassStructFor = _typeReferenceFactory.Create(classStructForName, null, namespaceName);

            return getGLibClassStructFor;
        }
    }
}

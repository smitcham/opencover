﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Mono.Cecil;
using OpenCover.Framework.Model;

namespace OpenCover.Framework.Strategy
{
    /// <summary>
    /// Track NUnit test methods
    /// </summary>
    public class TrackNUnitTestMethods : ITrackedMethodStrategy
    {
        public IEnumerable<TrackedMethod> GetTrackedMethods(IEnumerable<TypeDefinition> typeDefinitions)
        {
            return (from typeDefinition in typeDefinitions
                    from methodDefinition in typeDefinition.Methods
                    from customAttribute in methodDefinition.CustomAttributes
                    where customAttribute.AttributeType.FullName == "NUnit.Framework.TestAttribute"
                    select new TrackedMethod()
                    {
                        MetadataToken = methodDefinition.MetadataToken.ToInt32(),
                        Name = methodDefinition.FullName,
                        Strategy = "NUnitTest"
                    });
        }
    }
}

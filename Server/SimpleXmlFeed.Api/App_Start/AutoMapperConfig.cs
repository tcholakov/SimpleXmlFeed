﻿namespace SimpleXmlFeed.Api.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    using Infrastructure.Mappings;

    public static class AutoMapperConfig
    {
        public static void RegisterMappings(params Assembly[] assemblies)
        {
            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.GetExportedTypes());
            }
            
            Mapper.Initialize(config => {
                LoadStandardMappings(types, config);
                LoadCustomMappings(types, config);
            });
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression config)
        {
            var maps = types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                .Where(
                    type =>
                        type.i.IsGenericType && type.i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !type.t.IsAbstract
                        && !type.t.IsInterface)
                .Select(type => new { Source = type.i.GetGenericArguments()[0], Destination = type.t });

            foreach (var map in maps)
            {
                config.CreateMap(map.Source, map.Destination);
                config.CreateMap(map.Destination, map.Source);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression config)
        {
            var maps =
                types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                    .Where(
                        type =>
                            typeof(IHaveCustomMappings).IsAssignableFrom(type.t) && !type.t.IsAbstract &&
                            !type.t.IsInterface)
                    .Select(type => (IHaveCustomMappings)Activator.CreateInstance(type.t));

            foreach (var map in maps)
            {
                map.CreateMappings(config);
            }
        }
    }
}
using System.Xml;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace StudentHome.Core
{
    internal class ComponentInfo
    {
        public object Component;
        public Dictionary<string, string> Properties;
    }
    public class AppContext
    {
        private Dictionary<string, ComponentInfo> components;

        public AppContext(string fileName)
        {
            XmlReader reader = XmlReader.Create(fileName);
            ComponentInfo lastComponentInfo = null;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "root":
                                components = new Dictionary<string, ComponentInfo>();
                                break;
                            case "component":
                                string id = reader.GetAttribute("id");
                                string typeName = reader.GetAttribute("type");
                                string assemblyName = reader.GetAttribute("assembly");
                                Assembly assembly;

                                if (assemblyName == null)
                                    assembly = Assembly.GetEntryAssembly();
                                else
                                    if (assemblyName.Contains("UserInteface"))
                                        assembly = Assembly.LoadFrom(assemblyName + ".exe");
                                    else
                                        assembly = Assembly.LoadFrom(assemblyName + ".dll");

                                foreach (Type type in assembly.GetTypes())
                                {
                                    if (type.Name == typeName)
                                    {
                                        lastComponentInfo = new ComponentInfo();
                                        lastComponentInfo.Component = Activator.CreateInstance(type);
                                        lastComponentInfo.Properties = new Dictionary<string, string>();
                                        components[id] = lastComponentInfo;
                                    }
                                }

                                break;
                            case "property":
                                lastComponentInfo.Properties[reader.GetAttribute("name")] = reader.GetAttribute("ref");
                                break;
                            default:
                                break;
                        }
                        break;

                    case XmlNodeType.EndElement:
                        switch (reader.Name)
                        {
                            case "root":
                                foreach (string componentId in components.Keys)
                                {
                                    ComponentInfo componentInfo = components[componentId];

                                    foreach (string propertyName in componentInfo.Properties.Keys)
                                    {
                                        string fieldRefId = componentInfo.Properties[propertyName];
                                        Type type = componentInfo.Component.GetType();
                                        FieldInfo fieldInfo = type.GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
                                        fieldInfo.SetValue(componentInfo.Component, components[fieldRefId].Component);
                                    }
                                }
                                break;
                            case "component":
                                break;
                            case "property":
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public T GetComponent<T>(string name) where T : class
        {
            return components[name].Component as T;
        }
    }
}


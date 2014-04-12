using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client
{
    public class JsonAdapter
    {
        private Type _target;

        private IDictionary<string, PropertyInfo> _fieldMappings;
        
        private DataTypesMapper _dataTypesMapper;
        
        private IDictionary<string, string> _fieldsTypesMap = new Dictionary<string, string>();

        private static readonly IDictionary<Type, IDictionary<string, PropertyInfo>> _propertyMappings = new Dictionary<Type, IDictionary<string, PropertyInfo>>();

        private static object sync = new object();

        public JsonAdapter(Type target, DataTypesMapper dataTypesMapper)
        {
            _target = target;
            _dataTypesMapper = dataTypesMapper;
            initPropertyMappings();
        }

        private void initPropertyMappings()
        {
            lock (sync)
            {
                if (!_propertyMappings.ContainsKey(_target))
                {
                    var mappings = new Dictionary<string, PropertyInfo>();
                    foreach (var property in _target.GetRuntimeProperties())
                    {
                        var sodaProperty = property.GetCustomAttribute<SodaFieldAttribute>();
                        if (sodaProperty != null)
                        {
                            mappings.Add(sodaProperty.Value, property);
                        }
                        else
                        {
                            mappings.Add(property.Name, property);
                        }
                    }
                    _propertyMappings.Add(_target, mappings);
                }
            }
            _fieldMappings = _propertyMappings[_target];
        }

        public List<object> FromJsonArray(JArray items)
        {
            var result = new List<object>(items.Count);
            for (int i = 0; i < items.Count; i++)
            {
                var jsonObject = items[i];
                var item = FromJsonObject(jsonObject);
                result.Add(item);
            }
            return result;
        }

        public object FromJsonObject(JToken json)
        {
            if(_target.GetTypeInfo().GetCustomAttribute<SodaEntityAttribute>()==null)
            {
                throw new InvalidOperationException("Target is not annotated with @SodaEntity");
            }
            var model = Activator.CreateInstance(_target);
            foreach (var fieldTypeEntry in _fieldsTypesMap)
            {
                var field = fieldTypeEntry.Key;
                var type = fieldTypeEntry.Value;
                var targetField = _fieldMappings.ContainsKey(field) ? _fieldMappings[field] : null;
                if(targetField != null)
                {
                    var jsonValue = json[field];
                    if(jsonValue != null)
                    {
                        var value = _dataTypesMapper.GetValue(targetField, type, jsonValue);
                        targetField.SetValue(model, value);
                    }
                }
            }
            return model;
        }

        public void AddFieldMapping(string field, string type)
        {
            _fieldsTypesMap.Add(field, type);
        }
    }
}

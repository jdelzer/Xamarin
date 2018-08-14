using System;
using System.Collections.Generic;
using System.Reflection;

namespace JensCafeAndroid.Resources
{
    public class ResourceHelper
    {
        private static Dictionary<String, int> resourceDictionary = new Dictionary<string, int>();

        public static string[] StateList { get; set; } = new string[] {
            "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "GU",
            "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MH", "MI", "MN", "MO", "MS", "MT", "NC",
            "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "PW", "RI", "SC", "SD", "TN", "TX",
            "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY"};

        public static int TranslateDrawable(string drawableName)
        {
            int resourceValue = -1;

            if (resourceDictionary.ContainsKey(drawableName))
            {
                resourceValue = resourceDictionary[drawableName];
            }
            else
            {
                Type drawableType = typeof(Resource.Mipmap);

                FieldInfo resourceFieldInfo = drawableType.GetField(drawableName);

                resourceValue = (int)resourceFieldInfo.GetValue(null);

                resourceDictionary.Add(drawableName, resourceValue);
            }
            return resourceValue;
        }
    }
}
namespace ThonkBrain {
    public static class ExtensionMethods {

        public static void IncrementValueByID<T>(this Dictionary<T, int> keyValuePairs, T id, int incrementCount = 1) where T : notnull {
            if (keyValuePairs.ContainsKey(id)) {
                keyValuePairs[id] += incrementCount;
            } else {
                keyValuePairs.Add(id, incrementCount);
            }
        }
    }
}

public static class StringExt {
    /// <summary>
    /// Checks if two strings are equal while ignoring their case.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static bool EqualsIgnoreCase(this string s, string s2) {
        return s.ToLower().Equals(s2.ToLower());
    }
}
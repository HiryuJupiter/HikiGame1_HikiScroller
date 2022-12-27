public class Formulas {

    /// <summary>
    /// Calculates the amount of damage to be dealt. Currently this is just the strength stat.
    /// </summary>
    /// <param name="damager"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int CalculateDamage(Stats damager, Stats target) {
        return damager.Strength;


    }

}
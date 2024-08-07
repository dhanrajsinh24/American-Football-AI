using UnityEngine;

/// <summary>
/// Factory class to create NPC defenders.
/// </summary>
public class DefenderFactory : MonoBehaviour
{
    public static GameObject Create(GameObject defenderPrefab, int defenderIndex, PlayerInfo targetInfo, Vector3 position)
    {
        var defenderObject = Instantiate(defenderPrefab, position, Quaternion.identity);
        defenderObject.transform.SetParent(targetInfo.playerTransform.parent);
        var defender = defenderObject.GetComponent<Defender>();
        DefenderMovements defenderMovements = defender.DefenderMovements;

        // Initialize after Setting defender and target speeds
        float defenderSpeed = Random.Range(4f, 7f);
        PlayerInfo defenderInfo = new (defender.transform, defenderSpeed);
        
        // Initialize strategy for the defender
        Vector3 formationOffset = new (2, 0, 2);
        FormationDefense strategy = new (defenderMovements, defenderInfo, targetInfo, defenderIndex, formationOffset);
        
        // Initialize defender to activate it
        defender.Initialize(strategy, defenderSpeed);

        return defenderObject;
    }
}
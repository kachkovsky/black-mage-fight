using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomLevel : MonoBehaviour
{
    public Slider enemyHealth;
    public Slider heroHealth;
    public Slider hearts;
    public Slider teleports;
    public Slider heartHeal;

    public void Play() {
        GameManager.instance.NewGame(
            enemyHealth.Int(), 
            heroHealth.Int(), 
            teleports.Int(), 
            hearts.Int(), 
            heartHeal.Int()
        );
    }
}

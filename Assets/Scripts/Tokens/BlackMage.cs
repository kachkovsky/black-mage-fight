using UnityEngine;
using System.Collections;
using System.Linq;

public class BlackMage : Unit
{
    public static BlackMage instance;

    public int hitDamage = 1;

    void Awake() {
        instance = this;
    }

    public void Hit() {
        Hit(hitDamage);
        hitSound.time = 0.25f;
        hitSound.Play();
        if (health < 1) {
            Destroy(gameObject);
            GameManager.instance.Win();
        }
    }

    public void ResetDamageTokens() {
        Damage.damageTotal = 0;
        FindObjectsOfType<Damage>().ForEach(d => Destroy(d.gameObject));
        for (int i = 0; i < 3; i++) {
            Instantiate<Damage>(GameManager.instance.damage).Blink();
        }
    }

    public void HitWithDamageTokens() {
        if (Damage.damageTotal > 0) {
            Hit(Damage.damageTotal);
            hitSound.time = 0.25f;
            hitSound.Play();
        }
        ResetDamageTokens();
    }
}

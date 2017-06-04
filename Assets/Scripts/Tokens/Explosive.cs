using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RSG;
using System.Linq;

[RequireComponent(typeof(Figure))]
public class Explosive : MonoBehaviour
{
    Figure figure;
    public GameObject explosionSample;
    public AudioSource explosionSound;

    public void Awake() {
        figure = GetComponent<Figure>();
    }

    IPromise PlayExplosionAnimation(Cell from) {
        var explosion = Instantiate(explosionSample);
        explosion.SetActive(true);
        explosion.transform.position = from.transform.position;

        return TimeManager.Wait(0.1f).Then(() => {
            Destroy(explosion);
        });
    }

    void Explosion(Cell cell) {
        if (cell == null) {
            return;
        }
        if (cell.Figures.Count > 0) {
            cell.Figures.ForEach(f => {
                var onExplode = f.GetComponent<OnExplode>();
                if (onExplode) {
                    onExplode.run.Invoke();
                }
            });
        }
    }

    public void Explode() {
        if (!gameObject.activeSelf) {
            return;
        }
        this.TryPlay(explosionSound);
        var cell = figure.Position;
        gameObject.SetActive(false);
        Destroy(gameObject);
        PlayExplosionAnimation(cell);
        Explosion(cell.ToDirection(new IntVector2(1, 0)));
        Explosion(cell.ToDirection(new IntVector2(-1, 0)));
        Explosion(cell.ToDirection(new IntVector2(0, 1)));
        Explosion(cell.ToDirection(new IntVector2(0, -1)));
        Explosion(cell.ToDirection(new IntVector2(1, 1)));
        Explosion(cell.ToDirection(new IntVector2(-1, 1)));
        Explosion(cell.ToDirection(new IntVector2(1, -1)));
        Explosion(cell.ToDirection(new IntVector2(-1, -1)));
    }
}

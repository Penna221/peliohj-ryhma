using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    [SerializeField]
    GameObject iconPrefab;
    public List<Marker> markers = new List<Marker>();
    [SerializeField]
    RawImage compassImage;
    [SerializeField]
    Transform player;
    GameObject Marker;

    float compassUnit;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
    }
    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f , 0f, 1f, 1f);
        if (markers.Count > 0)
        {
            foreach (Marker marker in markers)
            {
                marker.image.rectTransform.anchoredPosition = getPosOnCompass(marker);
            }
        }
    }

    public void newMarker (Marker marker)
    {
        Marker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = Marker.GetComponent<Image>();
        marker.image.sprite = marker.icon;
        markers.Add(marker);
    }

    Vector2 getPosOnCompass (Marker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }

    public void newItem(GameObject gameObject)
    {
        Marker marker = gameObject.GetComponent<Marker>();
        newMarker(marker);
    }

    public void remoweItem()
    {
        Destroy(Marker);
        markers.Clear();
    }
}

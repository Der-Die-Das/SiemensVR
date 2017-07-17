using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScreen : MonoBehaviour
{

    #region HoverEffect
    public Transform[] wheels;
    public float spinSpeed = 2f;
    public float hoverStrength = 2f;
    public float hoverSpeed = 2f;
    #endregion
    #region Tutorial
    public Topic[] allTopics;
    private Topic activeTopic;
    public ScrollRect scrollRect;
    [Range(0,1)]
    public float asd;

    public RectTransform contentHolder;
    public GameObject TopicPrefab;
    private float oneItemHeight;
    #endregion



    // Use this for initialization
    void Start()
    {
        oneItemHeight = 1f / contentHolder.childCount;
        contentHolder.sizeDelta = new Vector2(contentHolder.sizeDelta.x,TopicPrefab.GetComponent<RectTransform>().sizeDelta.y/2f * contentHolder.childCount);
    }

    // Update is called once per frame
    void Update()
    {
        scrollRect.verticalNormalizedPosition = asd;
        foreach (var item in wheels)
        {
            item.GetChild(0).Rotate(Vector3.forward, spinSpeed * Time.deltaTime * 100f);
            item.Translate(Vector3.back * Mathf.Sin(Time.time * hoverSpeed) * hoverStrength * 2f);
        }
        transform.Translate(Vector3.up * Mathf.Sin(Time.time * hoverSpeed) * hoverStrength);
    }
}

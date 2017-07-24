using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerTemp : MonoBehaviour
{
    public LayerMask layer;
    public float toleranceForOutsideRecognition = 0.1f;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, layer))
        {
            if (hit.collider.gameObject.name == "UpperTopicBorder") //not the nicest way to solve..
            {
                hit.collider.GetComponentInParent<TutorialScreen>().Scroll(-1);
            }
            else if (hit.collider.gameObject.name == "LowerTopicBorder") //not the nicest way to solve..
            {
                hit.collider.GetComponentInParent<TutorialScreen>().Scroll(1);
            }
        }
    }
    [ContextMenu("Click")]
    public void Click()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100f);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f, layer);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                UITracked tracker = hit.collider.GetComponent<UITracked>();

                if (tracker != null)
                {
                    VRTopic topic = tracker.trackedUIObject.GetComponent<VRTopic>();
                    TutorialScreen screen = topic.GetComponentInParent<TutorialScreen>();
                    if (tracker.transform.position.y > screen.getLowerBorder().position.y - toleranceForOutsideRecognition && tracker.transform.position.y < screen.getUpperBorder().position.y + toleranceForOutsideRecognition)
                    {
                        screen.selectedTopic = topic;
                    }
                    break;
                }
            }
        }
    }
}

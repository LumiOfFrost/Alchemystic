using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCarry : MonoBehaviour
{

    public List<string> inv = new List<string>();
    public List<float> invWeights = new List<float>();
    public float carryWeight = 0;
    public float maxWeight = 30;
    bool itemInRange = false;
    GameObject curItem = null;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Item") && !itemInRange)
        {

            itemInRange = true;
            curItem = collision.gameObject;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Item") && itemInRange)
        {

            itemInRange = false;
            curItem = null;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        carryWeight = 0;
        inv.Clear();
    }

    private void Update()
    {

        for (int i = 0; i < inv.Count; i++) {
            if (GameObject.Find(i.ToString()) == null)
            {
                GameObject j = Instantiate(Resources.Load<GameObject>("CarryItem"), new Vector2(transform.position.x + 0.0625f, transform.position.y + 01.25f + (0.25f * i)), new Quaternion(0, 0, 0, 0), gameObject.transform);
                j.name = i.ToString();
                j.GetComponent<carriedItem>().ID = inv[i];
            }
        }

    }

    void OnInteract()
    {

        if (itemInRange && inv.Count < 10 && carryWeight + curItem.GetComponent<properties>().weight < maxWeight)
        {
            
            inv.Add(curItem.GetComponent<properties>().ID);
            invWeights.Add(curItem.GetComponent<properties>().weight);
            carryWeight += curItem.GetComponent<properties>().weight;
            itemInRange = false;
            GameObject.Destroy(curItem);
        } else if (carryWeight != 0)
        {
            GameObject item = Instantiate(Resources.Load<GameObject>("Item"), new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), new Quaternion(0, 0, 0, 0), this.transform.parent.parent);
            item.GetComponent<properties>().ID = inv[inv.Count - 1];
            item.GetComponent<properties>().weight = invWeights[invWeights.Count - 1];
            GameObject.Destroy(GameObject.Find((invWeights.Count - 1).ToString()));
            carryWeight -= invWeights[invWeights.Count - 1];
            invWeights.RemoveAt(invWeights.Count - 1);
            inv.RemoveAt(inv.Count - 1);
        }

    }

}
